using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Generator _generator;

    [SerializeField] private Mapdrawer _mapDrawer;

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
        Debug.Log("Manager start");
        Map = _generator.GenerateMap();
        _mapDrawer.Draw(Map);
    }

    void Update()
    {
        
    }
}
