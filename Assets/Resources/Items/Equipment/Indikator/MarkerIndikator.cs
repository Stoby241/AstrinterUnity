using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MarkerIndikator")]
public class MarkerIndikator : EquipmentItem
{
    GameObject imageObject;
    SpriteRenderer spriteRenderer;

    public Sprite arrowSprite;
    public KeyCode setPointButton = KeyCode.Mouse2;
    public Vector2 point = Vector2.zero;

    public override void selectedUpdate()
    {
        if (Input.GetKey(setPointButton))
        {
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public override void equipedUpdate()
    {
        imageObject.transform.position = (player.transform.position - (Vector3)point).normalized + player.transform.position;

        base.equipedUpdate();
    }

    public override void onEquiped()
    {
        imageObject = new GameObject();
        imageObject.transform.parent = player.inGameUI.transform;
        imageObject.transform.localPosition = new Vector3(0, 2, 0);
        imageObject.transform.localScale = new Vector3(3, 3, 0);
        imageObject.name = name + " Image";
        spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = arrowSprite;

        base.onEquiped();
    }

    public override void onUnequiped()
    {
        Destroy(imageObject);

        base.onUnequiped();
    }
}
