using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ToVelocityRotator")]
public class ToVelocityRotator : EquipmentItem
{
    public KeyCode button;

    public override void equipedUpdate()
    {
        base.equipedUpdate();

        if (Input.GetKey(button) && !player.grounded)
        {
            Vector2 force = player.GetComponent<Rigidbody>().velocity;
            player.transform.eulerAngles = new Vector3(0,0,Vector2.Angle(force, Vector2.up));
        }
    }
}
