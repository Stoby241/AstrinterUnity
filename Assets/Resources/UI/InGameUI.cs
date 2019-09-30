using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject addGameObject(string name, bool isRaltivetoPlayer, Vector2 pos)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = transform;
        gameObject.name = name;

        if (isRaltivetoPlayer)
            gameObject.transform.localPosition = pos;
        else
            gameObject.transform.position = pos;

        return gameObject;
    }
}
