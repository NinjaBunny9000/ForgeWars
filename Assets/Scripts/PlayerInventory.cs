using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<CraftedItem> craftedInventory;
    public CraftedItemDB craftedItemsDB;

    void Start()
    {
        craftedInventory = new List<CraftedItem>();
        // GiveCraftedItem(1);
    }

    public void GiveCraftedItem(int id)
    {
        CraftedItem itemToAdd = craftedItemsDB.GetItem(id);  // grab the item by it's id
        craftedInventory.Add(itemToAdd);
        Debug.Log("Added crafted item: " + itemToAdd.title);
    }

    public void GiveCraftedItem(string title)
    {
        CraftedItem itemToAdd = craftedItemsDB.GetItem(title);  // grab the item by it's id
        craftedInventory.Add(itemToAdd);
        Debug.Log("Added crafted item: " + itemToAdd.title);
    }

    public CraftedItem CheckForItem(int id) => craftedInventory.Find(item => item.id == id);
    public CraftedItem CheckForItem(string title) => craftedInventory.Find(item => item.title == title);

    public void RemoveItem(int id)
    {
        CraftedItem item = CheckForItem(id);
        if (item != null) {
            craftedInventory.Remove(item);
            Debug.Log("Item removed: " + item.title);
        }
    }


}
