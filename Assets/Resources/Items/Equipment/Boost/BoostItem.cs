using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoostItem : EquipmentItem
{
    public Vector2 force;
    public float torque;
    public KeyCode onButton;
    public bool goingRight;

    public override void equipedUpdate()
    {
        base.equipedUpdate();

        if (Input.GetKey(onButton) && !player.grounded)
        {
            player.rigidbody.AddRelativeForce(force);
            player.rotiatonVolcity += torque;
            player.goingRight = goingRight;
        }
    }
}
