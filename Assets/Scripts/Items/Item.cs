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
    
    //Adding GameObject for the Inventory
    [Header("Additional Attributes")]
    public GameObject itemObject;
    public GameObject categoryObject;
    public GameObject stackViewDisplay;
    public string categoryName;
    public string itemType;
    public string localName;

    /// <summary>
    /// Allows the item to be used in a certain way.
    /// </summary>
    public virtual void Use()
    {
        Debug.Log("item Used" + localName);
        // this script is required for the removing system.
    }

    /// <summary>
    /// Remove the item from the inventory.
    /// </summary>
    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveI(this);
    }
}