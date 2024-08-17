using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generator: ScriptableObject
{
    [SerializeField] private uint _size;
    public uint Size => _size;

    public abstract CellType[,] GenerateMap();

}
