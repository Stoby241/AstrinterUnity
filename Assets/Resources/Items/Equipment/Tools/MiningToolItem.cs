using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition); // gets Position IJ
        Vector2Int chunktargetIJ = GameMath.roundVector(GameMath.XYtoIJ(target) / player.creater.pointsSize) * player.creater.pointsSize;
        if (player.asteroidFieldData.chunks.ContainsKey(chunktargetIJ))
        {
            ChunkData clickedChunk = player.creater.asteroidFieldData.chunks[chunktargetIJ];

            Vector2Int pointtargetIJ = GameMath.roundVector(GameMath.XYtoIJ(target) - clickedChunk.startPosIJ);

            /*if (damTimer > 0) // updates timer for demage
            {
                damTimer -= Time.deltaTime;
            }

            if (clickedTriangle.visable)
            {
                if (Input.GetKey(onButton) && damTimer <= 0)
                {
                    damTimer = miningSpeed;
                    BuildItem minedItem = player.creater.removeDuribilityfromTriangle(clickedTriangle, digDemange);

                    if (minedItem != null)
                    {
                        player.inventory.addItem(minedItem);
                        player.inventory.addHotBarItem(1, minedItem);
                    }
                }
                clickedTriangle.color = selectedColor;
                player.creater.updateMesh(clickedTriangle.chunkData);

                if (lastTriangle != null)
                {
                    lastTriangle.color = new Color(0, 0, 0, 0);
                    player.creater.updateMesh(lastTriangle.chunkData);
                }
                lastTriangle = clickedTriangle;
            }*/
        }

        base.selectedUpdate();
    }
}
