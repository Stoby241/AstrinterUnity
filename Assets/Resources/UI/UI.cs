using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Canvas canvas;
    public Font font;

    public GameObject addText(string name, string text, int fontSize, Vector2 scale,
        AnchorPresets anchorPreset = AnchorPresets.MiddleCenter, 
        Vector2Int pos = new Vector2Int(), 
        PivotPresets pivotPreset = PivotPresets.MiddleCenter)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = canvas.transform;
        gameObject.name = name;

        Text textObject = gameObject.AddComponent<Text>();
        textObject.text = text;
        textObject.font = font;
        textObject.fontSize = fontSize;

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        RectTransformExtensions.SetAnchor(rectTransform, anchorPreset, pos.x, pos.y);
        RectTransformExtensions.SetPivot(rectTransform, pivotPreset);
        rectTransform.localScale = scale;

        return gameObject;
    }
    public GameObject addSprite(string name, Sprite sprite, Vector2 scale,
        AnchorPresets anchorPreset = AnchorPresets.MiddleCenter,
        Vector2Int pos = new Vector2Int(),
        PivotPresets pivotPreset = PivotPresets.MiddleCenter)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = canvas.transform;
        gameObject.name = name;

        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;

        RectTransform rectTransform = image.GetComponent<RectTransform>();
        RectTransformExtensions.SetAnchor(rectTransform, anchorPreset, pos.x, pos.y);
        RectTransformExtensions.SetPivot(rectTransform, pivotPreset);
        rectTransform.localScale = scale;

        return gameObject;
    }
}

