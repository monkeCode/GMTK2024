using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Generators/Default")]
public class DefaultGenerator : Generator
{

    [SerializeField] private int _riversCount=2;

    [SerializeField] private float _dodgeProbability = 0.3f;

    public override CellType[,] GenerateMap()
    {
        var map = new CellType[Size,Size];
        GenerateRiverMap(map);
        return map;
    }

    private void GenerateRiverMap(CellType[,] map)
    {
        int lastDirection = 0;
        for (int i = 0; i < _riversCount ; i++)
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


                int direction = Random.value > _dodgeProbability  ? lastDirection : Random.Range(0, 3);

                if (direction == 0 && startX > 0 && lastDirection != 1)
                    startX--;
                else if (direction == 1 && startX < map.GetLength(1) - 1 && lastDirection != 0)
                    startX++;
                else
                {
                    startY++;
                    direction = 2;
                }

                lastDirection = direction;
            }
        }
    }
}
