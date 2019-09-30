using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MiningToolItem")]
public class MiningToolItem : Item
{
    public KeyCode onButton = KeyCode.Mouse1;
    public float miningDistance;
    public float miningSpeed; // 1 = 1 per second
    public float digDemange;

    public Color selectedColor;

    Triangle lastTriangle;
    float damTimer;
    public override void selectedUpdate()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition); // gets Position

        Triangle clickedTriangle = null;
        float distance = float.MaxValue;
        foreach (ChunkData chunkData in player.creater.finishedRendertChunkObjectsIJ)
        {
            foreach (Triangle triangle in chunkData.triangles)
            {
                float newDistance = Vector2.Distance(triangle.middlePos + chunkData.startPosXY, target);
                if (distance > newDistance)
                {
                    distance = newDistance;
                    clickedTriangle = triangle;
                }
            }
        }

        if (damTimer > 0) // updates timer for demage
        {
            damTimer -= Time.deltaTime;
        }

        if (lastTriangle != null)
        {
            lastTriangle.color = new Color(0, 0, 0, 0);
            player.creater.updateMesh(lastTriangle.chunkData);
        }

        if (clickedTriangle != null && clickedTriangle.visable)
        {
            clickedTriangle.color = selectedColor;
            player.creater.updateMesh(clickedTriangle.chunkData);

            if (Input.GetKey(onButton) && damTimer <= 0)
            {
                damTimer = miningSpeed;
                BuildItem minedItem = player.creater.removeDuribilityfromTriangle(clickedTriangle, digDemange);

                if (minedItem != null)
                {
                    player.inventory.addItem(minedItem);
                }
            }
            player.creater.updateMesh(clickedTriangle.chunkData);
            player.creater.updateCollider(clickedTriangle.chunkData);

            lastTriangle = clickedTriangle;
        }

   base.selectedUpdate();
    }
}
