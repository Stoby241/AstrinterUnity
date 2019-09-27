using UnityEngine;

[CreateAssetMenu(menuName = "Items/PathIndikator")]
public class PathIndikator : EquipmentItem
{
    GameObject lineObject;
    LineRenderer lineRenderer;

    public float G;
    public float mass;
    public float minForceMagnitude;
    public float maxTraingleDistance;

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
                            float force = (G * mass * triangle.item.mass) / (distance * distance);
                            forceVector += relativPosition.normalized * force;
                        }
                    }
                }
                velocity += forceVector * forceOffset;
                points[i] = points[i - 1] + (Vector3)velocity;
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
        lineObject = new GameObject();
        lineObject.transform.parent = inGameUI.transform;
        lineObject.transform.position = new Vector3(0, 0, 10);
        lineObject.name = name + " line";
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = pointsAmmount + 1;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.material = material;

        G = player.physiksObject.G;
        mass = player.physiksObject.mass;
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
