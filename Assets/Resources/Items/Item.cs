using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public Sprite Icon;

    public int stackSize;
    public float weight;
    public string discription;
    
    public Player player;
    public InGameUI inGameUI;

    public virtual void equipedUpdate() { }
    public virtual void selectedUpdate() { }
    public virtual void onEquiped() { }
    public virtual void onUnequiped() { }
}
