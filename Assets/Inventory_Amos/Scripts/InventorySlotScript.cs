using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    private Stack<Item> items = new Stack<Item>();

    [SerializeField]
    private Image icon;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }
    public bool AddItem(Item item)
    {
        // Add an item/multiple similar items to the slot.
        items.Push(item);
        icon.sprite = item.itemArt;
        icon.color = Color.white;

        return true;
    }

    /*
    public Item RemoveItem(Item item)
    {
        return items.Remove(item);
    }
    */
}
