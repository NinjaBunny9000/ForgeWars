using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedItem
{

    public int id;
    public string title;
    // public string description;
    // public Sprite icon; 
    // public Dictionary<string,int> stats = new Dictionary<string, int>();
    public Dictionary<string,int> stats;

 
    // constructor
    public CraftedItem(int id, string title, Dictionary<string,int> stats)
    {
        this.id = id;
        this.title = title;
        // this.description = description;
        // this.icon = Resources.Load<Sprite>("Materials/Sprites/CraftedItems/" + title);
        this.title = title;
    }


    public CraftedItem(CraftedItem craftedItem)
    {
        this.id = craftedItem.id;
        this.title = craftedItem.title;
        // this.description = craftedItem.description;
        // this.icon = Resources.Load<Sprite>("Materials/Sprites/CraftedItems/" + title);
        this.stats = craftedItem.stats;
    }
}
