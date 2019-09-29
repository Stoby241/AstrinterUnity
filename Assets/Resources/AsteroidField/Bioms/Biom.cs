using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bioms/Biom")]
public class Biom : ScriptableObject
{
    public BuildItem[] items;
    public Vector4[] settings;
    public Vector2[] noiseOffsets;
}
