using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapdrawer : MonoBehaviour
{
    [SerializeField] private TileBase _ground;
    [SerializeField] private TileBase _river;
    [SerializeField] private TileBase _road;
    [SerializeField] private Tilemap _tilemap;

    public void Draw(CellType[,] logicMap)
    {
        for(int i = 0; i < logicMap.GetLength(0); i++)
        for (int j = 0; j < logicMap.GetLength(1); j++)
        {
            var tile = logicMap[i, j] switch
            {
                CellType.River => _river,
                CellType.Road => _road,
                _ => _ground
            };

            //int z = (logicMap[i, j] == CellType.River)? 999:j+i;

            _tilemap.SetTile(new Vector3Int(i,j, 0), tile);

            Debug.Log($"new tile at ({i},{j})");

        }
    }

    void Start()
    {
        //var m = new CellType[5, 5];
        //for (int i = 0; i < m.GetLength(0); i++)
        //{
         //   m[1, i] = CellType.River;
        //}
        //Draw(m);
    }
}



