using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ShoesItem")]
public class ShoesItem : EquipmentItem
{
    public KeyCode walkLeftButton;
    public KeyCode walkRightButton;
    public KeyCode jumpButton;

    public float walkSpeed;
    public float jumpSpeed;

    public float minGravityMultiplyer;
    public float negativGavityOffset;

    public override void equipedUpdate()
    {
        base.equipedUpdate();

        if (player.grounded)
        {
            float walkOffset = (float)Math.Sqrt(player.physiksObject.forceVector.magnitude);
            if (Input.GetKey(walkLeftButton))
            {
                player.rigidbody.AddRelativeForce(Vector2.left * walkSpeed, (ForceMode2D)ForceMode.Acceleration);
                player.goingRight = false;
            }
            else if (Input.GetKey(walkRightButton))
            {
                player.rigidbody.AddRelativeForce(Vector2.right * walkSpeed, (ForceMode2D)ForceMode.Acceleration);
                player.goingRight = true;
            }
            if (Input.GetKeyDown(jumpButton))
            {
                player.rigidbody.AddRelativeForce(Vector2.up * jumpSpeed, (ForceMode2D)ForceMode.Acceleration);
            }

            Vector2 force = player.physiksObject.forceVector;
            if (force.magnitude > player.physiksObject.minForceMagnitude * minGravityMultiplyer)
            {
                float angle = Vector2.Angle(force, Vector2.down);
                if (force.x < 0)
                    angle = 360 - angle;

                if (330 < angle && angle < 360 || 0 < angle && angle < 30)
                {
                    angle = 0;
                }
                else if (30 < angle && angle < 90)
                {
                    angle = 58;
                }
                else if (90 < angle && angle < 150)
                {
                    angle = 122;
                }
                else if (150 < angle && angle < 210)
                {
                    angle = 180;
                }
                else if (210 < angle && angle < 270)
                {
                    angle = 238;
                }
                else if (270 < angle && angle < 330)
                {
                    angle = 302;
                }

                float rotation = player.transform.eulerAngles.z;

                if (rotation < 0)
                    rotation = 360 - rotation;



                if (rotation < angle + 4 && rotation > angle - 4)
                {
                    player.rotiatonVolcity = 0;
                    player.transform.eulerAngles = new Vector3(0, 0, angle);
                }
                else
                {
                    if (rotation < angle)
                    {
                        float a = angle - rotation;
                        float b = 360 - a;

                        if (a < b)
                        {
                            player.rotiatonVolcity = 2;
                        }
                        else
                        {
                            player.rotiatonVolcity = -2;
                        }

                    }
                    else if (rotation > angle)
                    {
                        float a = rotation - angle;
                        float b = 360 - a;

                        if (a < b)
                        {
                            player.rotiatonVolcity = -2;
                        }
                        else
                        {
                            player.rotiatonVolcity = 2;
                        }
                    }
                }
            }
        }
        else if (player.wasgrounded)
        {
            player.rotiatonVolcity *= 0.5f;
        }
    }
}