using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Generators/Default")]
public class DefaultGenerator : Generator
{

    [SerializeField] private float _riverScale = 10;

    [SerializeField] [Range(0,1)] private float _riverMax = 0.5f;
    [SerializeField] [Range(0,1)] private float _riverMin = 0.4f;

    public override CellType[,] GenerateMap()
    {
        var map = new CellType[Size,Size];
        GenerateRiverMap(map);
        return map;
    }

    private static void GenerateRiverMap(CellType[,] map)
    {

        for (int i = 0; i < 4; i++)
        {
            bool fromUp = Random.Range(0, 2) == 1;
            int startX=Random.Range(0, map.GetLength(0));
            int startY = 0;

            while (startY < map.GetLength(1) - 1)
            {
                if (fromUp)
                    map[startY, startX] = CellType.River;
                else
                    map[startX, startY] = CellType.River;

                int direction = Random.Range(0, 4);
                if (direction == 0 && startX > 0) startX--;
                else if (direction == 1 && startX < map.GetLength(1) - 1) startX++;
                else startY++;
            }
        }
    }

    private void GenerateRivers(CellType[,] map)
    {
        var offsetX = Random.value * 1000;
        var offsetY = Random.value* 1000;
        for(int i =0; i < map.GetLength(0); i++)
        for (int j = 0; j < map.GetLength(1); j++)
        {
            var p  = Mathf.PerlinNoise(i/(float)map.GetLength(0)*_riverScale + offsetX, j/(float)map.GetLength(1)*_riverScale+offsetY);
            if (p >= _riverMin && p <= _riverMax)
                map[i, j] = CellType.River;
        }
    }
}
