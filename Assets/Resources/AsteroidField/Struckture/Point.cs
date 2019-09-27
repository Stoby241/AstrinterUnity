using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public AsteroidFieldData asteroidFieldData;
    public ChunkData chunkData;
    public Vector2Int posIJ;
    public Vector2 posXY;
    public Triangle[] triangles;
    public bool hasVisableTriangle;
}
