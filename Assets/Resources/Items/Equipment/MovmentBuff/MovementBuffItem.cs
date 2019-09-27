using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementBuffItem : EquipmentItem
{
    public Vector2 movmentBuff;
    public float torqueBuff;
    public KeyCode onButton;

    public override void equipedUpdate()
    {
        base.equipedUpdate();

        if (Input.GetKey(onButton))
        {
            player.rigidbody.velocity *= movmentBuff;
            player.rotiatonVolcity *= torqueBuff;
        }
    }
}
