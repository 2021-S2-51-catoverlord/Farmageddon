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

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveI(this);
    }
}