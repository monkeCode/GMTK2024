using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapMapper
{
    private static List<Square> SquareSqlit(Square square)
    {
       if (square.Height < 1 || square.Width < 1)
           throw new ArithmeticException($"h {square.Height}, w {square.Width}");
       if (square.Height < 4 || square.Width < 4)
           return new List<Square>() { square };

       bool horizontal = (Random.Range(0, 2) == 1) && square.Height > 3;

       if (horizontal)
       {
           var x = Random.Range(2, square.Width-1);
           if (x - 1 + square.XStart < square.XStart || x + 1+ square.XStart > square.XEnd)
               throw new AggregateException();

           var l = SquareSqlit(square with { XEnd = x - 1 + square.XStart });
           l.AddRange(SquareSqlit(square with { XStart = x + 1 + square.XStart }));
           return l;
       }

       var y = Random.Range(2, square.Height-1);
       if (y - 1 + square.YStart < square.YStart || y +1+ square.YStart > square.YEnd)
           throw new AggregateException();

       var l2 = SquareSqlit(square with { YEnd = y - 1 + square.YStart });
       l2.AddRange(SquareSqlit(square with { YStart = y +1 + square.YStart }));
       return l2;
    }

    public static (HashSet<Vector2Int>, HashSet<Vector2Int>) GetHousesAndRoads(int width, int height)
    {
        var squares = SquareSqlit(new Square() { XStart = 1, XEnd = width-1, YStart = 1, YEnd = height-1 });
        byte[,] map = new byte[width, height];
        foreach (var square in squares)
        {

            for(int x=square.XStart; x <= square.XEnd; x++)
            for (int y = square.YStart; y <= square.YEnd; y++)
            {
                map[x, y] = 1;
            }
        }

        var housePoints = new HashSet<Vector2Int>();
        var roadPoints = new HashSet<Vector2Int>();
        for(int x=0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            if (map[x, y] == 1)
            {
                housePoints.Add(new Vector2Int(x, y));
            }
            else
            {
                roadPoints.Add(new Vector2Int(x, y));
            }
        }

        return (housePoints, roadPoints);
    }

}

public record Square
{
    public int XStart { get; set; }
    public int XEnd { get; set; }
    public int YStart { get; set; }
    public int YEnd { get; set; }

    public int Width => XEnd - XStart + 1;
    public int Height => YEnd - YStart + 1;
}
