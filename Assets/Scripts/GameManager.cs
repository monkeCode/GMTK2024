using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Generator _generator;

    [SerializeField] private Mapdrawer _mapDrawer;

    [SerializeField] private GameObject testHome;

    private HashSet<Vector2Int> _allowedBuildPositions = new HashSet<Vector2Int>();

    public static GameManager Instance { get; private set; }

    private List<Vector2> _hoisePositions = new List<Vector2>();
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
        //TODO: Проверять на речку на этой точке
        _allowedBuildPositions.Add(new Vector2Int(Map.GetLength(1) / 2, Map.GetLength(0) / 2));
        BuildHouse();
    }

    private Vector2Int PopBuildPosition()
    {
        var i = Random.Range(0, _allowedBuildPositions.Count);
        var point = _allowedBuildPositions.Skip(i).First();
        Map[point.x, point.y] = CellType.House;

        for (int x = Mathf.Max(point.x - 2, 0); x < Mathf.Min(point.x + 2, Map.GetLength(1)); x++)
        {
            var p1 = new Vector2Int(x, Mathf.Min(point.y + 2, Map.GetLength(0)));
            var p2 = new Vector2Int(x, Mathf.Min(point.y - 2,Map.GetLength(0)));
            if (Map[p1.x, p1.y] == CellType.Empty)
                _allowedBuildPositions.Add(p1);
            if (Map[p2.x, p2.y] == CellType.Empty)
                _allowedBuildPositions.Add(p2);
        }
        var px = new Vector2Int(Mathf.Max(point.x - 2, 0), point.y);
        var py = new Vector2Int(Mathf.Min(point.x + 2, Map.GetLength(0)), point.y);
        if (Map[px.x, px.y] == CellType.Empty)
            _allowedBuildPositions.Add(px);
        if (Map[py.x, py.y] == CellType.Empty)
            _allowedBuildPositions.Add(py);
        return point;
    }

    private void CenterCamera()
    {
        var x = _hoisePositions.Select(it => it.x).Sum() / _hoisePositions.Count;
        var y = _hoisePositions.Select(it => it.y).Sum() / _hoisePositions.Count;
        _camera.transform.position = new Vector3(x, y, _camera.transform.position.z);
    }
    public void BuildHouse()
    {
        var pos = PopBuildPosition();
        _mapDrawer.DrawHouse(testHome, pos.x, pos.y);
        var convPos = Mapdrawer.ConvertCoord(pos.x, pos.y);
        _hoisePositions.Add(new Vector2(convPos.Item1, convPos.Item2 ));
        CenterCamera();
    }
}
