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

    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _millPrefab;
    [SerializeField] private GameObject _towerPrefab;

    private HashSet<Vector2Int> _allowedBuildPositions = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> _closestBuildPositions = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> _allowedRoadPositions = new HashSet<Vector2Int>();

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
        DontDestroyOnLoad(gameObject);
        _camera = Camera.main;
    }

    void Start()
    {
        Map = _generator.GenerateMap();
        _mapDrawer.Draw(Map);
         (_allowedBuildPositions, _allowedRoadPositions) = MapMapper.GetHousesAndRoads(Map.GetLength(1), Map.GetLength(0));
        //_mapDrawer.DrawRoad(_allowedRoadPositions);
        BuildStartHouses();
       // BuildHouse();
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

        for (int x = Mathf.Max(point.x - 2, 0); x < Mathf.Min(point.x + 2, Map.GetLength(0)); x++)
        {
            for (int y = Mathf.Max(point.y - 2, 0); y < Mathf.Min(point.y + 2, Map.GetLength(1)); y++)
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
        return BuildSmth(_housePrefab);
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
        return BuildSmth(_towerPrefab);
    }

    public Vector2Int BuildMill()
    {
        return BuildSmth(_millPrefab);
    }

    class RoadNode
    {
        public Vector2Int Position { get; set; }
        public RoadNode Last { get; set; }
    }

    private void BuildRoads(Vector2Int fromPoint)
    {
         var startRoadPoint = fromPoint;
         if (startRoadPoint.x > 0 && _allowedRoadPositions.Contains(new Vector2Int(fromPoint.x - 1, fromPoint.y)))
         {
             startRoadPoint.x -= 1;
         }
         else if ((startRoadPoint.x < Map.GetLength(0)-1) && _allowedRoadPositions.Contains(new Vector2Int(fromPoint.x + 1, fromPoint.y)))

         {
             startRoadPoint.x += 1;
         }
         else if (startRoadPoint.y > 0 && _allowedRoadPositions.Contains(new Vector2Int(fromPoint.x, fromPoint.y-1)))
         {
             startRoadPoint.y -= 1;
         }
         else if ((startRoadPoint.y < Map.GetLength(1)-1) && _allowedRoadPositions.Contains(new Vector2Int(fromPoint.x, fromPoint.y+1)))
         {
             startRoadPoint.y += 1;
         }

         if(startRoadPoint == fromPoint)
             return;

         if (!_allowedRoadPositions.Remove(startRoadPoint))
             throw new Exception("Incorrect start point");

         Map[startRoadPoint.x, startRoadPoint.y] = CellType.Road;
        _mapDrawer.DrawRoad(new List<Vector2Int>(){startRoadPoint});
         var start = new RoadNode() { Position = startRoadPoint, Last = null };
         var roadsPoints = new HashSet<Vector2Int>(_allowedRoadPositions);
         Queue<RoadNode> nodes = new Queue<RoadNode>();
         nodes.Enqueue(start);
         RoadNode end = null;
         while (nodes.Count > 0 && end == null)
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
                         end = n;
                         break;
                     }

                     if(roadsPoints.Remove(new Vector2Int(x,y)))
                     {
                         var r = new RoadNode() { Position = new Vector2Int(x, y), Last = n };
                         nodes.Enqueue(r);

                     }

                 }
             }
         }

         if (end == null)
             return;

         HashSet<Vector2Int> roads = new HashSet<Vector2Int>();
         roads.Add(startRoadPoint);
         while (end.Last != null)
         {
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

}
