using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedItemDB : MonoBehaviour
{
    public List<CraftedItem> items;

    void Awake()
    {
        BuildDatabase();
    }

    void Start()
    {
        
    }

    public CraftedItem GetItem(int id) => items.Find(item => item.id == id);
    public CraftedItem GetItem(string itemTitle) => items.Find(item => item.title == itemTitle);


    void BuildDatabase()
    {
        items = new List<CraftedItem>() {
            new CraftedItem(0, "dagger", new Dictionary<string,int>{
                {"valueMin", 15},
                {"valueMax", 30}
            }),
            new CraftedItem(1, "sword", new Dictionary<string,int>{
                {"valueMin", 30},
                {"valueMax", 50}
            }),
            new CraftedItem(2, "shield", new Dictionary<string,int>{
                {"valueMin", 40},
                {"valueMax", 60}
            })
        };
    }


    

}
