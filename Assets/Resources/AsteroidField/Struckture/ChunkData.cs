using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{
    public AsteroidFieldData asteroidFieldData;
    public Vector2Int startPosIJ;
    public Vector2 startPosXY;
    public Point[,] points;
    public Triangle[,] triangles;
    public List<Triangle> visableTriangle;

    public Vector3[] vertices;
    public int[] indices;
    public Vector2[] uv;
    public Vector2[] uv1;
    public Color[] colors;

    public List<Vector2[]> colliderPaths;

    public int state;

    public Mesh mesh;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public PolygonCollider2D polygonCollider2D;

    public ChunkContainer chunkContainer;
}

public class ChunkContainer : MonoBehaviour
{
    public ChunkData chunkData;
    [SerializeField] Vector2 startPosIJ;
    [SerializeField] Vector2 startPosXY;

    private void Start()
    {
        startPosIJ = chunkData.startPosIJ;
        startPosXY = chunkData.startPosXY;
    }
}
