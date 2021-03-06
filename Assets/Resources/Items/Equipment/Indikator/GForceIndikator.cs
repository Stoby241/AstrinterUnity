﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/GForceIndikator")]
public class GForceIndikator : EquipmentItem
{ 
    GameObject imageObject;
    SpriteRenderer spriteRenderer;

    float wantedAngle;
    float currentAngle;

    public override void equipedUpdate()
    {
        wantedAngle = Vector2.Angle(player.physiksObject.forceVector, Vector2.up);
        if(player.physiksObject.forceVector.x > 0)
        {
            wantedAngle = 360 - wantedAngle;
        }
        wantedAngle = wantedAngle - player.transform.eulerAngles.z;

        imageObject.transform.RotateAround(player.transform.position, Vector3.forward, wantedAngle - currentAngle);
        currentAngle = currentAngle + (wantedAngle - currentAngle);

        base.equipedUpdate();
    }

    public override void onEquiped()
    {
        imageObject = player.inGameUI.addGameObject(name + " Image", true, new Vector3(0, 2, 0));
        imageObject.transform.localScale = new Vector3(3, 3, 0);
        spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load("Images/UI/RedArrow", typeof(Sprite)) as Sprite;

        base.onEquiped();
    }

    public override void onUnequiped()
    {
        Destroy(imageObject);

        base.onUnequiped();
    }
}
