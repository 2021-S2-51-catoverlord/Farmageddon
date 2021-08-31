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
    internal Sprite icon;

    [SerializeField]
    public Sprite itemArt
    {
        get
        {
            return itemArt;
        }
    }

    protected InventorySlotScript slot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }


    public Item(string itemName, int ID, string description)
    {
        this.itemName = itemName;
        this.ID = ID;
        this.description = description;
    }
}