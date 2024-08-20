using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Generator _generator;

    [SerializeField] private Mapdrawer _mapDrawer;

    [SerializeField] private List<GameObject> _housePrefabs;
    [SerializeField] private List<GameObject> _millPrefabs;
    [SerializeField] private List<GameObject> _towerPrefabs;

    private HashSet<Vector2Int> _allowedBuildPositions = new();
    private HashSet<Vector2Int> _closestBuildPositions = new();
    private HashSet<Vector2Int> _allowedRoadPositions = new();

    public static GameManager Instance { get; private set; }

    private HashSet<Vector2> _housePositions = new HashSet<Vector2>();

    private Camera _camera;

    public CellType[,] Map;

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
       // DontDestroyOnLoad(gameObject);
        _camera = Camera.main;
    }


    private void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        Map = _generator.GenerateMap();
        _mapDrawer.Draw(Map);
         (_allowedBuildPositions, _allowedRoadPositions) = MapMapper.GetHousesAndRoads(Map.GetLength(1), Map.GetLength(0));
        //_mapDrawer.DrawRoad(_allowedRoadPositions);
        BuildStartHouses();
    }

    void BuildStartHouses()
    {
        var x = Map.GetLength(1) / 2;
        var y = Map.GetLength(0) / 2;

        while (!_allowedBuildPositions.Contains(new Vector2Int(x,y)) || Map[x,y] !=CellType.Empty)
        {
            x += Random.Range(0, 3)-1;
            y += Random.Range(0, 3) - 1;
        }

        _closestBuildPositions.Add(new Vector2Int(x, y));
        BuildHouse();
        BuildMill();
        BuildTower();
    }

    private Vector2Int PopBuildPosition()
    {
        var i = Random.Range(0, _closestBuildPositions.Count);
        var point = _closestBuildPositions.Skip(i).First();
        _closestBuildPositions.Remove(point);
        Map[point.x, point.y] = CellType.House;

        for (int x = Mathf.Max(point.x - 2, 0); x < Mathf.Min(point.x + 3, Map.GetLength(0)); x++)
        {
            for (int y = Mathf.Max(point.y - 2, 0); y < Mathf.Min(point.y + 3, Map.GetLength(1)); y++)
            {
                var vec = new Vector2Int(x, y);
                if (_allowedBuildPositions.Contains(vec) && Map[x,y] == CellType.Empty)
                {
                    _closestBuildPositions.Add(vec);
                }
            }
        }
        return point;
    }

    private void CenterCamera()
    {
        var x = _housePositions.Select(it => it.x).Sum() / _housePositions.Count;
        var y = _housePositions.Select(it => it.y).Sum() / _housePositions.Count;
        _camera.transform.position = new Vector3(x, y, _camera.transform.position.z);
    }

    public Vector2Int BuildHouse()
    {
        return BuildSmth(_housePrefabs[Random.Range(0, _housePrefabs.Count)]);
    }

    private Vector2Int BuildSmth(GameObject prefab)
    {
        var pos = PopBuildPosition();
        _mapDrawer.DrawHouse(prefab, pos.x, pos.y);
        var convPos = Mapdrawer.ConvertCoord(pos.x, pos.y);
        _housePositions.Add(new Vector2(convPos.Item1, convPos.Item2));
        CenterCamera();
        BuildRoads(pos);
        return pos;
    }

    public Vector2Int BuildTower()
    {
        return BuildSmth(_towerPrefabs[Random.Range(0, _towerPrefabs.Count)]);
    }

    public Vector2Int BuildMill()
    {
        var pos =  BuildSmth(_millPrefabs[Random.Range(0, _millPrefabs.Count)]);
        var wheat = Random.Range(0, 2) == 1;
        const float buildProba = 0.7f;

        void BuildCulture(int xS, int yS)
        {
            List<Vector2Int> poses = new List<Vector2Int>();
            for(int x = Mathf.Max(0, xS-1); x < Mathf.Min(xS+2, Map.GetLength(0)); x++)
            {
                for (int y = Mathf.Max(0, yS - 1); y < Mathf.Min(yS + 2, Map.GetLength(1)); y++)
                {
                    if (Map[x, y] != CellType.Empty || Mathf.Abs(x-xS) == Mathf.Abs(y-yS) || !_allowedBuildPositions.Contains(new Vector2Int(x,y))) continue;

                    poses.Add(new Vector2Int(x,y));

                }
            }

            if (poses.Count ==0)
                return;
            var newPos = poses[Random.Range(0, poses.Count)];
            Map[newPos.x, newPos.y] = CellType.House;

            if (wheat)
                _mapDrawer.DrawWheat(newPos.x, newPos.y);
            else
                _mapDrawer.DrawCorn(newPos.x, newPos.y);

            _allowedBuildPositions.Remove(newPos);
            _closestBuildPositions.Remove(newPos);

            if (Random.value < buildProba)
            {
                BuildCulture(newPos.x, newPos.y);
            }
        }

        BuildCulture(pos.x, pos.y);

        return pos;
    }

    class RoadNode
    {
        public Vector2Int Position { get; set; }
        public RoadNode Last { get; set; }

        public int Deep { get; set; }
    }

    private void BuildRoads(Vector2Int fromPoint)
    {
         var startRoadPoint = fromPoint;

         for (int x = Mathf.Max(fromPoint.x - 1, 0); x < Mathf.Min(fromPoint.x + 2, Map.GetLength(0)); x++)
         {
             for (int y = Mathf.Max(fromPoint.y - 1, 0); y < Mathf.Min(fromPoint.y + 2, Map.GetLength(1)); y++)
             {

                 if(Mathf.Abs(x-fromPoint.x) == Mathf.Abs(y- fromPoint.y))
                     continue;

                 if(Map[x,y] == CellType.Road)
                     return;

                 if (_allowedRoadPositions.Contains(new Vector2Int(x, y)))
                 {
                     startRoadPoint = new Vector2Int(x, y);
                     break;
                 }
             }
         }

         if(startRoadPoint == fromPoint)
             return;

         if (!_allowedRoadPositions.Remove(startRoadPoint))
             throw new Exception("Incorrect start point");

         if (Map[startRoadPoint.x, startRoadPoint.y] == CellType.River)
         {
             _mapDrawer.DrawBridge(startRoadPoint.x, startRoadPoint.y, Map);
         }
         else
         {
             _mapDrawer.DrawRoad(new List<Vector2Int>(){startRoadPoint});
         }
         Map[startRoadPoint.x, startRoadPoint.y] = CellType.Road;

         var start = new RoadNode() { Position = startRoadPoint, Last = null, Deep = 0};
         var roadsPoints = new HashSet<Vector2Int>(_allowedRoadPositions);
         Queue<RoadNode> nodes = new Queue<RoadNode>();
         nodes.Enqueue(start);
         RoadNode end = null;

         while (nodes.Count > 0 /*&& end == null*/)
         {
             var n = nodes.Dequeue();

             for (int x = Mathf.Max(n.Position.x - 1, 0); x < Mathf.Min(n.Position.x + 2, Map.GetLength(0)); x++)
             {
                 for (int y = Mathf.Max(n.Position.y - 1, 0); y < Mathf.Min(n.Position.y + 2, Map.GetLength(1)); y++)
                 {
                     var positionX = x - n.Position.x;
                     var positionY = y  - n.Position.y;

                     if(Mathf.Abs(positionX) == Mathf.Abs(positionY))
                         continue;

                     if (Map[x, y] == CellType.Road && x != startRoadPoint.x && y != startRoadPoint.y)
                     {
                         end = (end is null || end.Deep >  n.Deep)?n:end;
                         //break;
                         continue;
                     }

                     if(roadsPoints.Remove(new Vector2Int(x,y)))
                     {
                         var r = new RoadNode() { Position = new Vector2Int(x, y), Last = n, Deep = n.Deep+1};
                         nodes.Enqueue(r);
                     }
                 }
             }
         }

         if (end == null)
             return;

         HashSet<Vector2Int> roads = new HashSet<Vector2Int>();
         //roads.Add(startRoadPoint);
         while (end.Last != null)
         {

             if (Map[end.Position.x, end.Position.y] == CellType.River)
             {
                 _mapDrawer.DrawBridge(end.Position.x, end.Position.y, Map);
             }
             else
                 roads.Add(end.Position);

             Map[end.Position.x, end.Position.y] = CellType.Road;
             if (!_allowedRoadPositions.Remove(new Vector2Int(end.Position.x, end.Position.y)))
             {
                 throw new ArgumentException("incorect road point");
             }
             end = end.Last;
         }

         _mapDrawer.DrawRoad(roads);
    }

    public void StartWinter()
    {
        _mapDrawer.DrawWinter();
    }

    public void EndWinter()
    {
        _mapDrawer.DrawSpring();
    }

}
