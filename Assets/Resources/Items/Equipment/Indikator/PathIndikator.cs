using UnityEngine;

[CreateAssetMenu(menuName = "Items/PathIndikator")]
public class PathIndikator : EquipmentItem
{
    GameObject lineObject;
    LineRenderer lineRenderer;

    public float G;
    public float objectMass;
    public float minForceMagnitude;
    public float maxTraingleDistance;
    public float minTraingleDistance;

    public int pointsAmmount;
    public float forceOffset;
    public float velocityOffset;
    public float pointsOffset;
    public Color startColor;
    public Color endColor;
    public float startWidth;
    public float endWidth;
    public Material material;

    Vector3[] points;

    public override void equipedUpdate()
    {
        if (!player.grounded && player.creater.finishedRendertChunkObjectsIJ != null)
        {
            lineObject.SetActive(true);

            points = new Vector3[pointsAmmount + 1];
            points[0] = player.transform.position;

            Vector2 velocity = player.rigidbody.velocity * velocityOffset;
            Vector2 forceVector;

            bool stop = false;
            int i = 1;
            while (i < pointsAmmount + 1)
            {

                forceVector = Vector2.zero;
                foreach (ChunkData chunkData in player.creater.finishedRendertChunkObjectsIJ)
                {
                    foreach (Triangle triangle in chunkData.visableTriangle)
                    {
                        Vector2 relativPosition = (triangle.middlePos + chunkData.startPosXY) - (Vector2)points[i - 1];
                        float distance = relativPosition.magnitude;

                        if (distance < maxTraingleDistance)
                        {
                            float force = (G * objectMass * triangle.item.mass) / (distance * distance);
                            forceVector += relativPosition.normalized * force;

                            if (distance < minTraingleDistance)
                            {
                                stop = true;
                            }
                        }
                    }
                }
                velocity += forceVector * forceOffset;

                if (!stop)
                {
                    points[i] = points[i - 1] + (Vector3)velocity;
                }
                else
                {
                    points[i] = points[i - 1];
                }
                

                i++;
            }

            lineRenderer.SetPositions(points);
        }
        else
        {
            lineObject.SetActive(false);
        }
        
        base.equipedUpdate();
    }

    public override void onEquiped()
    {
        lineObject = player.inGameUI.addGameObject(name, true, Vector2.zero);
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = pointsAmmount + 1;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.material = material;

        G = player.physiksObject.G;
        objectMass = player.physiksObject.mass;
        minForceMagnitude = player.physiksObject.minForceMagnitude;
        maxTraingleDistance = player.physiksObject.maxTraingleDistance;

        base.onEquiped();
    }

    public override void onUnequiped()
    {
        Destroy(lineObject);

        base.onUnequiped();
    }
}
