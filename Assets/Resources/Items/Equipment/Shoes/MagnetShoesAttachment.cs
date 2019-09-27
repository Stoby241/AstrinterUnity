using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagnetShoesAttachment : EquipmentItem
{
    public KeyCode triggerButton;
    public Vector2 magnetForce;
    public bool enabled;

    public override void equipedUpdate()
    {
        base.equipedUpdate();

        if (Input.GetKey(triggerButton))
            enabled = !enabled;


        if (player.grounded && enabled)
        {
            player.GetComponent<Rigidbody>().AddRelativeForce(magnetForce);
        }
    }
}