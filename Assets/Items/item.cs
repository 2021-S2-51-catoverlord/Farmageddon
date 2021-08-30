using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int ID;
    public string description;
    public int price;
    public Sprite itemArt;

    public Item(string itemName, int ID, string description)
    {
        this.itemName = itemName;
        this.ID = ID;
        this.description = description;
    }

}
