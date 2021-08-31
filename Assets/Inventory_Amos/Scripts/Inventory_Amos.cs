using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Amos : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}
