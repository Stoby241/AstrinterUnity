using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysiksObject : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Creater creater;

    public float G;
    public float mass;
    public float minForceMagnitude;
    public float maxTraingleDistance;

    public Vector2 forceVector;

    private void FixedUpdate()
    {
        if(creater.finishedRendertChunkObjectsIJ != null)
        {
            forceVector = Vector2.zero;
            foreach (ChunkData chunkData in creater.finishedRendertChunkObjectsIJ)
            {
                foreach (Triangle triangle in chunkData.visableTriangle)
                {
                    Vector2 relativPosition = (triangle.middlePos + chunkData.startPosXY) - (Vector2)transform.position;
                    float distance = relativPosition.magnitude;

                    if (distance < maxTraingleDistance)
                    {
                        float force = (G * mass * triangle.item.mass) / (distance * distance);
                        forceVector += relativPosition.normalized * force;
                    }
                }
            }

            if(forceVector.magnitude < minForceMagnitude)
            {
                forceVector = Vector2.zero;
            }

            rigidbody.AddForce(forceVector, (ForceMode2D)ForceMode.Acceleration);
        }
    }
}
