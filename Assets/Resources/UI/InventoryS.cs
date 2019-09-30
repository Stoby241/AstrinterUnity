using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryS : MonoBehaviour
{
    public Player player;
    public UI UI;

    [SerializeField] List<Item> items;
    public void addItem(Item item)
    {
        item.player = player;
        item.UI = UI;
        items.Add(item);
    }
    public void removeItem(Item item)
    {
        item.player = null;
        items.Remove(item);
    }

    [SerializeField] Item[] equipedItems;
    public void addEquipedItem(int slot, Item item)
    {
        equipedItems[slot] = item;

        item.onEquiped();
    }
    public Item changeEquipedItem(int slot, Item item)
    {
        Item preItem = equipedItems[slot];
        equipedItems[slot] = item;

        item.onEquiped();
        preItem.onUnequiped();

        return preItem;
    }
    public Item removeEquipedItem(int slot)
    {
        Item preItem = equipedItems[slot];
        equipedItems[slot] = null;

        preItem.onUnequiped();

        return preItem;
    }

    [SerializeField] Item[] hotBar;
    public void addHotBarItem(int slot, Item item)
    {
        hotBar[slot] = item;
    }
    public Item changeHotBarItem(int Slot, Item item)
    {
        Item preItem = hotBar[Slot];
        hotBar[Slot] = item;
        return preItem;
    }
    public Item removeHotBarItem(int slot)
    {
        Item preItem = hotBar[slot];
        hotBar[slot] = null;
        return preItem;
    }
    public int selectedSlot;
    public Item getselectedItem()
    {
        return hotBar[selectedSlot];
    }

    [SerializeField] Item[] addtoEquiped;
    private void Start()
    {
        int i = 0;
        foreach(Item item in addtoEquiped)
        {
            addItem(item);
            addEquipedItem(i, item);
            i++;
        }
    }

    private void FixedUpdate()
    {
        updateEquipedItems();
        updateSelectedItem();
    }
    public void updateEquipedItems()
    {
        foreach(Item item in equipedItems)
        {
            if(item != null)
                item.equipedUpdate();
        }
    }
    public void updateSelectedItem()
    {
        Item item = hotBar[selectedSlot];
        if (item != null)
            item.selectedUpdate();
    }
}
