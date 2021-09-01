using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public int ID = 0;
    public string description = "Add Description";
    public int price = 0;
    public Sprite icon = null;

    public Item(string itemName, int ID, string description)
    {
        this.itemName = itemName;
        this.ID = ID;
        this.description = description;
    }
}