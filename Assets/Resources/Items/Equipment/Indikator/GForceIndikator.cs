using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
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
        imageObject = new GameObject();
        imageObject.transform.parent = inGameUI.transform;
        imageObject.transform.localPosition = new Vector3(0, 2, 0);
        imageObject.transform.localScale = new Vector3(3, 3, 0);
        imageObject.name = name + " Image";
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
