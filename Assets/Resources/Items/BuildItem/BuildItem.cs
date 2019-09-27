using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/BuildItem")]
public class BuildItem : Item
{
    public float duribility;
    public float maxduribility;
    public float mass;
    public Vector2[] UV0s;

    public KeyCode onButton = KeyCode.Mouse1;
    public float buildDistance = 5f;
    public float searchChunkDistanceOffset = 2f;
    public float searchTriangleDistanceOffset = 2f;
    public Vector2 target;
    public List<Triangle> triangleList;

    Triangle lastTriangle;
    /*public override void selectedUpdate()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition); // gets Position
        chunkList = new List<Chunk>();
        triangleList = new List<Triangle>();

        chunkList.AddRange( // gets all near chunks
            player.chunkList.FindAll(
                Chunk =>
                Vector3.Distance(Chunk.gameObject.transform.position, target) < player.asteroidFieldCreater.triangelLayers * searchChunkDistanceOffset &&
                Vector3.Distance(player.transform.position, target) < buildDistance
            )
        );

        foreach (Chunk chunk in chunkList)
        {
            triangleList.AddRange( // gets all near triangles
                chunk.triangleList.FindAll(
                    Triangle =>
                    Vector3.Distance(Triangle.pos, target) < searchTriangleDistanceOffset &&
                    Vector3.Distance(player.transform.position, target) < buildDistance
                )
            );
        }

        float distance = float.PositiveInfinity;
        Triangle clickedTriangle = player.chunkList[0].triangleList[0];

        foreach (Triangle triangle in triangleList) // gets the neares triangle
        {
            float newDistance = Vector2.Distance(triangle.pos, target);

            if (newDistance < distance)
            {
                distance = newDistance;
                clickedTriangle = triangle;
            }
        }

        if (lastTriangle != null)
        {
            lastTriangle.visable = false;
            clickedTriangle.chunk.visableTriangleList.Remove(clickedTriangle);
            player.asteroidFieldCreater.updateMesh(lastTriangle.chunk);
            lastTriangle = null;
        }

        if (!clickedTriangle.visable)
        {
            if (Input.GetKey(onButton))
            {
                clickedTriangle.item = this;
                player.asteroidFieldCreater.setTriangleVisable(clickedTriangle);
                player.inventory.removeItem(this);
            }
            else
            {
                clickedTriangle.UV0s = UV0s;
                clickedTriangle.visable = true;
                clickedTriangle.chunk.visableTriangleList.Add(clickedTriangle);
                player.asteroidFieldCreater.updateMesh(clickedTriangle.chunk);

                lastTriangle = clickedTriangle;
            }
        }

        base.selectedUpdate();
    }*/
}
