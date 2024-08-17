using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Generator _generator;

    [SerializeField] private Mapdrawer _mapDrawer;

    [SerializeField] private GameObject testHome;

    public static GameManager Instance { get; private set; }

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
    }

    void Start()
    {
        Map = _generator.GenerateMap();
        _mapDrawer.Draw(Map);
        _mapDrawer.DrawHouse(testHome, 0,0);
        _mapDrawer.DrawHouse(testHome, 0,1);
        _mapDrawer.DrawHouse(testHome, 1,1);
        _mapDrawer.DrawHouse(testHome, 1,5);
        _mapDrawer.DrawRoad( new List<Vector2Int>
        {
            new Vector2Int(1,2),
            new Vector2Int(2,2),
            new Vector2Int(3,2),
            new Vector2Int(3,3),

        });
    }

    void Update()
    {
        
    }
}
