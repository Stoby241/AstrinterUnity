using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public AsteroidFieldData asteroidFieldData;
    public ChunkData chunkData;
    public bool pointingUP;
    public Point[] points;
    public Vector2 middlePos;
    public bool visable;
    public BuildItem item;
    public Vector2[] UV0s;
    public Vector2[] UV2s;
    public Color color;
    public float perlinNoiseSamplePercent;
    public Biom biom;
}
