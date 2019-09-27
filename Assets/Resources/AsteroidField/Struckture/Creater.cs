using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    public AsteroidFieldData asteroidFieldData;
    public ThreadController threadController;
    public System.Random random;
    public int seed;
    void Start()
    {
        if (seed == 0)
        {
            System.Random seedRandom = new System.Random();
            seed = seedRandom.Next(int.MinValue, int.MaxValue);
        }
        random = new System.Random(seed);

        for (int i = 0; i < visableNoiseSettings.Length; i++)
        {
            visableNoiseSettings[i].z = random.Next(0, noiseOffset.x);
            visableNoiseSettings[i].w = random.Next(0, noiseOffset.y);
        }
        for (int i = 0; i < biomNoiseSettings.Length; i++)
        {
            biomNoiseSettings[i].z = random.Next(0, noiseOffset.x);
            biomNoiseSettings[i].w = random.Next(0, noiseOffset.y);
        }

        asteroidFieldData.chunks = new Dictionary<Vector2Int, ChunkData>();
        rendertChunkObjectsIJ = new ChunkData[renderRadius * 2, renderRadius * 2];
        finishedRendertChunkObjectsIJ = new List<ChunkData>();
        chunkDatasList0 = new List<ChunkData>();

        createUVs();
    }

    public BuildItem[] defaultAsteroidLayers;
    public Texture2D texture0;
    public Texture2D texture1;
    public int texHeight;
    public int texWidth;
    public Vector2[] UV0s;
    public Vector2[] UV2s;
    private void createUVs()
    {
        int width = texture0.width;
        int heigth = texture0.height;
        int ammount = width / texWidth;

        UV0s = new Vector2[ammount * 6];
        int i = 0;
        while (i * 6 < UV0s.Length)
        {
            UV0s[i * 6 + 2] = new Vector2((float)i * (float)texWidth / (float)width, 0);
            UV0s[i * 6 + 0] = new Vector2(((float)i + 1f) * (float)texWidth / (float)width, 0);
            UV0s[i * 6 + 1] = new Vector2(((float)i + 0.5f) * (float)texWidth / (float)width, 0.84f);

            UV0s[i * 6 + 3] = new Vector2((float)i * (float)texWidth / (float)width, 0.84f);
            UV0s[i * 6 + 4] = new Vector2(((float)i + 1f) * (float)texWidth / (float)width, 0.84f);
            UV0s[i * 6 + 5] = new Vector2(((float)i + 0.5f) * (float)texWidth / (float)width, 0);

            i++;
        }

        i = 0;
        while (i < defaultAsteroidLayers.Length)
        {
            BuildItem item = defaultAsteroidLayers[i];
            item.UV0s = new Vector2[6];

            item.UV0s[0] = UV0s[i * 6];
            item.UV0s[1] = UV0s[i * 6 + 1];
            item.UV0s[2] = UV0s[i * 6 + 2];
            item.UV0s[3] = UV0s[i * 6 + 3];
            item.UV0s[4] = UV0s[i * 6 + 4];
            item.UV0s[5] = UV0s[i * 6 + 5];
            i++;
        }

        width = texture1.width;
        heigth = texture1.height;
        ammount = width / texWidth;

        UV2s = new Vector2[ammount * 6];
        i = 0;
        while (i * 6 < UV2s.Length)
        {
            UV2s[i * 6 + 2] = new Vector2((float)i * (float)texWidth / (float)width, 0);
            UV2s[i * 6 + 1] = new Vector2(((float)i + 1f) * (float)texWidth / (float)width, 0);
            UV2s[i * 6 + 0] = new Vector2(((float)i + 0.5f) * (float)texWidth / (float)width, 0.84f);

            UV2s[i * 6 + 3] = new Vector2((float)i * (float)texWidth / (float)width, 0.84f);
            UV2s[i * 6 + 4] = new Vector2(((float)i + 1f) * (float)texWidth / (float)width, 0.84f);
            UV2s[i * 6 + 5] = new Vector2(((float)i + 0.5f) * (float)texWidth / (float)width, 0);

            i++;
        }
    }

    public Player player;
    public int renderRadius;
    public ChunkData[,] rendertChunkObjectsIJ;
    public List<ChunkData> finishedRendertChunkObjectsIJ;
    public ChunkData[,] wasRendertChunkObjectsIJ;
    List<ChunkData> chunkDatasList0;
    bool creationRunnig0;
    public bool showAllChunks;
    bool wasShowAllChunks;
    public int finishCallsPerUpadate;
    void Update()
    {
        int i = 0;
        if (wasRendertChunkObjectsIJ != null && !showAllChunks)
        {
            foreach (ChunkData chunkData in wasRendertChunkObjectsIJ)
            {
                if (chunkData.state >= 9)
                    chunkData.chunkContainer.gameObject.SetActive(false);
            }
        }

        finishedRendertChunkObjectsIJ.Clear();
        bool creationNeeded = false;
        player.posIJ = GameMath.XYtoIJ(player.transform.position);
        for (i = 0; i < renderRadius * 2; i++)
        {
            for (int j = 0; j < renderRadius * 2; j++)
            {
                Vector2Int chunkPosIJ = new Vector2Int(
                    ((int)(player.posIJ.x / pointsSize) + i - renderRadius) * pointsSize,
                    ((int)(player.posIJ.y / pointsSize) + j - renderRadius) * pointsSize);

                ChunkData chunkData;
                if (!asteroidFieldData.chunks.ContainsKey(chunkPosIJ))
                {
                    chunkData = new ChunkData();
                    chunkData.startPosIJ = chunkPosIJ;
                    chunkData.state = 1;
                    asteroidFieldData.chunks.Add(chunkPosIJ, chunkData);
                }
                else
                {
                    chunkData = asteroidFieldData.chunks[chunkPosIJ];
                }
                rendertChunkObjectsIJ[i, j] = chunkData;

                if (chunkData.state < 5)
                {
                    creationNeeded = true;
                }
                else
                {
                    finishedRendertChunkObjectsIJ.Add(chunkData);
                }
            }
        }

        i = 0;
        if (creationNeeded)
        {
            foreach (ChunkData chunkData in rendertChunkObjectsIJ)
            {
                switch (chunkData.state)
                {
                    case 1:
                        chunkData.state++;
                        chunkDatasList0.Add(chunkData);
                        break;
                    case 3:
                        if(i < finishCallsPerUpadate)
                        {
                            chunkData.state++;
                            finishChunk(chunkData);
                            i++;
                        }
                        break;
                }
            }
        }
        if (!creationRunnig0 && chunkDatasList0.Count != 0)
        {
            creationRunnig0 = true;
            ChunkData[] chunkDatas = new ChunkData[chunkDatasList0.Count];
            chunkDatasList0.CopyTo(chunkDatas);
            chunkDatasList0.Clear();

            threadController.startThreadedTask(() => createChunkDatas(chunkDatas));
        }

        if (!showAllChunks)
        {
            if (wasShowAllChunks)
            {
                foreach (ChunkData chunkData in asteroidFieldData.chunks.Values)
                {
                    if (chunkData.state >= 9)
                    {
                        chunkData.chunkContainer.gameObject.SetActive(false);
                    }
                }
                wasShowAllChunks = false;
            }

            foreach (ChunkData chunkData in rendertChunkObjectsIJ)
            {
                if (chunkData.state >= 9)
                    chunkData.chunkContainer.gameObject.SetActive(true);
            }

            wasRendertChunkObjectsIJ = rendertChunkObjectsIJ;
        }
        else
        {
            if (!wasShowAllChunks)
            {
                foreach (ChunkData chunkData in asteroidFieldData.chunks.Values)
                {
                    if (chunkData.state >= 9)
                    {
                        chunkData.chunkContainer.gameObject.SetActive(true);
                    }
                }
                wasShowAllChunks = true;
            }
        }

        if (!player.gameObject.active && !creationNeeded)
        {
            player.gameObject.SetActive(true);
        }
    }

    public int pointsSize;
    public Vector2Int noiseOffset;
    public float[,] visableNoiseSettings;
    public Biom[] bioms;
    public float[,] biomNoiseSettings;
    private void createChunkDatas(ChunkData[] chunkDatas)
    {
        for (int index = 0; index < chunkDatas.Length; index++)
        {
            ChunkData chunkData = chunkDatas[index];

            chunkData.startPosXY = GameMath.IJtoXY(chunkData.startPosIJ);
            chunkData.asteroidFieldData = asteroidFieldData;

            int i = 0;
            int pointsSizeplusOne = pointsSize + 1;
            chunkData.points = new Point[pointsSizeplusOne, pointsSizeplusOne];
            for (i = 0; i < pointsSizeplusOne; i++)
            {
                for (int j = 0; j < pointsSizeplusOne; j++)
                {
                    Vector2Int ij = new Vector2Int(i, j);
                    createPoint(chunkData, ij);
                }
            }

            chunkData.triangles = new Triangle[(pointsSizeplusOne - 1) * 2, pointsSizeplusOne - 1];
            for (int j = 0; j < pointsSizeplusOne; j++)
            {
                int iCounter = 0;
                for (i = 0; i < pointsSizeplusOne; i++)
                {
                    Point point = chunkData.points[i, j];
                    if (i > 0 && j < pointsSizeplusOne - 1)
                    {
                        createTriangle(chunkData, new Vector2Int(i, j), iCounter, false, point);
                        iCounter++;
                    }
                    if (i < pointsSizeplusOne - 1 && j < pointsSizeplusOne - 1)
                    {
                        createTriangle(chunkData, new Vector2Int(i, j), iCounter, true, point);
                        iCounter++;
                    }
                }
            }

            // Turning the triangles visable 
            foreach (Triangle triangle in chunkData.triangles)
            {
                for (i = 0; i < visableNoiseSettings.Length; i++)
                {
                    float xCoord = (triangle.middlePos.x + triangle.chunkData.startPosXY.x + visableNoiseSettings[i,2]) / visableNoiseSettings[i,1];
                    float yCoord = (triangle.middlePos.y + triangle.chunkData.startPosXY.y + visableNoiseSettings[i,3]) / visableNoiseSettings[i,1];
                    float noise = Mathf.PerlinNoise(xCoord, yCoord);

                    if (visableNoiseSettings[i,0] > noise)
                    {
                        triangle.visable = true;

                        float a = 1 - (noise / visableNoiseSettings[i,0]);
                        if (triangle.perlinNoiseSamplePercent < a)
                            triangle.perlinNoiseSamplePercent = a;
                    }
                    else if ((1 - visableNoiseSettings[i,0]) < noise)
                    {
                        triangle.visable = true;
                        float a = 1 - ((1 - noise) / visableNoiseSettings[i,0]);
                        if (triangle.perlinNoiseSamplePercent < a)
                            triangle.perlinNoiseSamplePercent = a;
                    }

                    if (triangle.perlinNoiseSamplePercent < 0)
                    {
                        triangle.perlinNoiseSamplePercent = 0;
                    }
                    else if (triangle.perlinNoiseSamplePercent > 0.999f)
                    {
                        triangle.perlinNoiseSamplePercent = 0.999f;
                    }
                }
            }

            chunkData.visableTriangle = new List<Triangle>();
            foreach (Triangle triangle in chunkData.triangles)
            {
                if (triangle.visable)
                {
                    chunkData.visableTriangle.Add(triangle);
                }
            }

            //Biom
            foreach (Triangle triangle in chunkData.visableTriangle)
            {
                float noise = 0;
                for (i = 0; i < biomNoiseSettings.Length; i++)
                {
                    float xCoord = (triangle.middlePos.x + triangle.chunkData.startPosXY.x + biomNoiseSettings[i,1]) / biomNoiseSettings[i,0];
                    float yCoord = (triangle.middlePos.y + triangle.chunkData.startPosXY.y + biomNoiseSettings[i,2]) / biomNoiseSettings[i,0];
                    float newnoise = Mathf.PerlinNoise(xCoord, yCoord);
                    
                    if(newnoise > noise)
                    {
                        noise = newnoise;
                        triangle.biom = bioms[i];
                    }
                    i++;

                }
                
            }

            // Assing Items
            foreach (Triangle triangle in chunkData.visableTriangle)
            {
                i = 0;
                foreach(BuildItem item in triangle.biom.items)
                {
                    if(triangle.perlinNoiseSamplePercent > triangle.biom.settings[i].y &&
                        triangle.perlinNoiseSamplePercent < triangle.biom.settings[i].z)
                    {
                        float b = random.Next(0,1);
                        if(b < triangle.biom.settings[i].x)
                        {
                            triangle.item = item;
                        }
                    }
                }
            }

            // Mesh
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            int indecesCounter = 0;
            List<Vector2> uv = new List<Vector2>();
            List<Vector2> uv2 = new List<Vector2>();
            List<Color> Colors = new List<Color>();
            foreach (Triangle triangle in chunkData.visableTriangle) // looping throw every visable trinagle.
            {
                if (triangle.item != null)
                {
                    triangle.UV0s = new Vector2[3];
                    if (triangle.pointingUP)
                    {
                        triangle.UV0s[0] = triangle.item.UV0s[0];
                        triangle.UV0s[1] = triangle.item.UV0s[1];
                        triangle.UV0s[2] = triangle.item.UV0s[2];
                    }
                    else
                    {
                        triangle.UV0s[2] = triangle.item.UV0s[3];
                        triangle.UV0s[1] = triangle.item.UV0s[4];
                        triangle.UV0s[0] = triangle.item.UV0s[5];
                    }
                }
                if (triangle.item != null)
                {
                    triangle.UV2s = new Vector2[3];

                    float a = triangle.item.duribility / triangle.item.maxduribility * 100;
                    a = 100 - a;
                    float b = triangle.UV2s.Length / 6;
                    float c = a * b / 100;
                    int uvNr = (int)c;

                    if (triangle.pointingUP)
                    {
                        triangle.UV2s[0] = UV2s[uvNr * 6];
                        triangle.UV2s[1] = UV2s[uvNr * 6 + 1];
                        triangle.UV2s[2] = UV2s[uvNr * 6 + 2];
                    }
                    else
                    {
                        triangle.UV2s[2] = UV2s[uvNr * 6 + 3];
                        triangle.UV2s[1] = UV2s[uvNr * 6 + 4];
                        triangle.UV2s[0] = UV2s[uvNr * 6 + 5];
                    }
                }

                vertices.Add(new Vector3(triangle.points[0].posXY.x, triangle.points[0].posXY.y, 0));
                vertices.Add(new Vector3(triangle.points[1].posXY.x, triangle.points[1].posXY.y, 0));
                vertices.Add(new Vector3(triangle.points[2].posXY.x, triangle.points[2].posXY.y, 0));

                indices.Add(indecesCounter);
                indices.Add(indecesCounter + 1);
                indices.Add(indecesCounter + 2);
                indecesCounter += 3;

                uv.Add(triangle.UV0s[0]);
                uv.Add(triangle.UV0s[1]);
                uv.Add(triangle.UV0s[2]);

                uv2.Add(triangle.UV2s[0]);
                uv2.Add(triangle.UV2s[1]);
                uv2.Add(triangle.UV2s[2]);

                Colors.Add(triangle.color);
                Colors.Add(triangle.color);
                Colors.Add(triangle.color);
            }
            chunkData.vertices = vertices.ToArray();
            chunkData.indices = indices.ToArray();
            chunkData.uv = uv.ToArray();
            chunkData.uv2 = uv2.ToArray();
            chunkData.colors = Colors.ToArray();

            // Collider
            chunkData.colliderPaths = new List<Vector2[]>();
            i = 0;
            foreach (Triangle triangle in chunkData.visableTriangle)
            {
                Vector2[] pos = new Vector2[3];
                pos[0] = triangle.points[0].posXY;
                pos[1] = triangle.points[1].posXY;
                pos[2] = triangle.points[2].posXY;

                chunkData.colliderPaths.Add(pos);
                i++;
            }
            chunkData.state++;
        }
        creationRunnig0 = false;
    }
    private void createPoint(ChunkData chunkData, Vector2Int pointPosIJ)
    {
        Point point = new Point();
        point.asteroidFieldData = chunkData.asteroidFieldData;
        point.chunkData = chunkData;
        point.posIJ = pointPosIJ;
        point.posXY = GameMath.IJtoXY(pointPosIJ);
        point.triangles = new Triangle[6];

        chunkData.points[pointPosIJ.x, pointPosIJ.y] = point;
    }
    private void createTriangle(ChunkData chunkData, Vector2Int trianglePosIJ, int iCounter, bool pointingUP, Point startPoint)
    {
        Triangle triangle = new Triangle();
        triangle.asteroidFieldData = chunkData.asteroidFieldData;
        triangle.chunkData = chunkData;
        triangle.pointingUP = pointingUP;

        triangle.points = new Point[3];
        triangle.points[0] = startPoint;
        if (pointingUP)
        {
            triangle.points[1] = chunkData.points[startPoint.posIJ.x, startPoint.posIJ.y + 1];
            triangle.points[2] = chunkData.points[startPoint.posIJ.x + 1, startPoint.posIJ.y];

            triangle.points[0].triangles[1] = triangle;
            triangle.points[1].triangles[3] = triangle;
            triangle.points[2].triangles[5] = triangle;
        }
        else
        {
            triangle.points[1] = chunkData.points[startPoint.posIJ.x - 1, startPoint.posIJ.y + 1];
            triangle.points[2] = chunkData.points[startPoint.posIJ.x, startPoint.posIJ.y + 1];

            triangle.points[0].triangles[0] = triangle;
            triangle.points[1].triangles[2] = triangle;
            triangle.points[2].triangles[4] = triangle;
        }

        triangle.middlePos = (triangle.points[0].posXY + triangle.points[1].posXY + triangle.points[2].posXY) / 3;

        triangle.visable = false;

        triangle.UV0s = new Vector2[3];

        triangle.UV2s = new Vector2[3];

        triangle.color = new Color();

        chunkData.triangles[iCounter, trianglePosIJ.y] = triangle;
    }

    public Material buildMaterial;
    private void finishChunk(ChunkData chunkData)
    {
        GameObject chunkObject = new GameObject();
        chunkObject.name = "Chunk PosIJ:" + chunkData.startPosIJ.x + " " + chunkData.startPosIJ.y;
        chunkObject.transform.parent = asteroidFieldData.gameObject.transform;
        chunkObject.transform.position = new Vector3(chunkData.startPosXY.x, chunkData.startPosXY.y, 0);

        ChunkContainer chunkContainer = chunkObject.AddComponent<ChunkContainer>();
        chunkData.chunkContainer = chunkContainer;
        chunkContainer.chunkData = chunkData;

        foreach (Triangle triangle in chunkData.visableTriangle)
        {
            BuildItem copyItem = triangle.item;
            triangle.item = (BuildItem)ScriptableObject.CreateInstance("BuildItem");
            triangle.item.Icon = copyItem.Icon;
            triangle.item.stackSize = copyItem.stackSize;
            triangle.item.weight = copyItem.weight;
            triangle.item.discription = copyItem.discription;
            triangle.item.player = copyItem.player;
            triangle.item.inGameUI = copyItem.inGameUI;
            triangle.item.duribility = copyItem.duribility;
            triangle.item.maxduribility = copyItem.maxduribility;
            triangle.item.UV0s = copyItem.UV0s;
            triangle.item.mass = copyItem.mass;
        }

        chunkData.mesh = new Mesh();
        chunkData.mesh.vertices = chunkData.vertices;
        chunkData.mesh.triangles = chunkData.indices;
        chunkData.mesh.uv = chunkData.uv;
        chunkData.mesh.uv2 = chunkData.uv2;
        chunkData.mesh.colors = chunkData.colors;

        chunkData.meshFilter = chunkObject.AddComponent<MeshFilter>();
        chunkData.meshFilter.mesh = chunkData.mesh;

        chunkData.meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        chunkData.meshRenderer.material = buildMaterial;

        chunkData.polygonCollider2D = chunkObject.AddComponent<PolygonCollider2D>();
        chunkData.polygonCollider2D.pathCount = chunkData.colliderPaths.Count;
        int i = 0;
        foreach (Vector2[] path in chunkData.colliderPaths)
        {
            chunkData.polygonCollider2D.SetPath(i, path);
            i++;
        }

        chunkData.state++;
    }

    public void updateMesh(ChunkData chunkData)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        int indecesCounter = 0;
        List<Vector2> uv = new List<Vector2>();
        List<Vector2> uv2 = new List<Vector2>();
        List<Color> Colors = new List<Color>();
        foreach (Triangle triangle in chunkData.visableTriangle) // looping throw every visable trinagle.
        {
            if (triangle.item != null)
            {
                triangle.UV0s = new Vector2[3];
                if (triangle.pointingUP)
                {
                    triangle.UV0s[0] = triangle.item.UV0s[0];
                    triangle.UV0s[1] = triangle.item.UV0s[1];
                    triangle.UV0s[2] = triangle.item.UV0s[2];
                }
                else
                {
                    triangle.UV0s[2] = triangle.item.UV0s[3];
                    triangle.UV0s[1] = triangle.item.UV0s[4];
                    triangle.UV0s[0] = triangle.item.UV0s[5];
                }
            }
            if (triangle.item != null)
            {
                triangle.UV2s = new Vector2[3];

                float a = triangle.item.duribility / triangle.item.maxduribility * 100;
                a = 100 - a;
                float b = triangle.UV2s.Length / 6;
                float c = a * b / 100;
                int uvNr = (int)c;

                if (triangle.pointingUP)
                {
                    triangle.UV2s[0] = UV2s[uvNr * 6];
                    triangle.UV2s[1] = UV2s[uvNr * 6 + 1];
                    triangle.UV2s[2] = UV2s[uvNr * 6 + 2];
                }
                else
                {
                    triangle.UV2s[2] = UV2s[uvNr * 6 + 3];
                    triangle.UV2s[1] = UV2s[uvNr * 6 + 4];
                    triangle.UV2s[0] = UV2s[uvNr * 6 + 5];
                }
            }

            vertices.Add(new Vector3(triangle.points[0].posXY.x, triangle.points[0].posXY.y, 0));
            vertices.Add(new Vector3(triangle.points[1].posXY.x, triangle.points[1].posXY.y, 0));
            vertices.Add(new Vector3(triangle.points[2].posXY.x, triangle.points[2].posXY.y, 0));

            indices.Add(indecesCounter);
            indices.Add(indecesCounter + 1);
            indices.Add(indecesCounter + 2);
            indecesCounter += 3;

            uv.Add(triangle.UV0s[0]);
            uv.Add(triangle.UV0s[1]);
            uv.Add(triangle.UV0s[2]);

            uv2.Add(triangle.UV2s[0]);
            uv2.Add(triangle.UV2s[1]);
            uv2.Add(triangle.UV2s[2]);

            Colors.Add(triangle.color);
            Colors.Add(triangle.color);
            Colors.Add(triangle.color);
        }
        chunkData.mesh.vertices = vertices.ToArray();
        chunkData.mesh.triangles = indices.ToArray();
        chunkData.mesh.uv = uv.ToArray();
        chunkData.mesh.uv2 = uv2.ToArray();
        chunkData.mesh.colors = Colors.ToArray();
    }
    public BuildItem removeDuribilityfromTriangle(Triangle clickedTriangle, float digDemange)
    {
        clickedTriangle.item.duribility -= digDemange;
        if(clickedTriangle.item.duribility <= 0)
        {
            clickedTriangle.item.duribility = clickedTriangle.item.maxduribility;
            return clickedTriangle.item;
        }
        else
        {
            return null;
        }
    }
}