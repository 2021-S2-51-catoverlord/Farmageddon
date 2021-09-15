using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedInventory : MonoBehaviour
{
    #region Singleton

    public static LimitedInventory instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public static int NUM_SLOTS = 12; // Number of slots the limited inventory bar has.
    public List<Item> items = new List<Item>(); // Current list of items in the limited inventory bar.

    /// <summary>
    /// Add new item, if enough room return true, else return false.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddI(Item item)
    {
        // If the inventory already has the item...
        if (items.Contains(item))
        {
            // Access the collection and modify item count by incrementing it.
            items[items.IndexOf(item)].itemAmount++;
            //++item.itemAmount;  
        }
        else if (!items.Contains(item)) // If the collection does not have the item to be added...
        {
            // Check if maximum capacity has been reached for the inventory...
            if (items.Count >= NUM_SLOTS)
            {
                Debug.Log("Not enough room");
                return false;
            }
            else // If there are still slots available...
            {
                // Add it to the collection.
                items.Add(item);
            }
        }

        if (onItemChangedCallback != null)
        {
            // Notify listeners that an item change has happened.
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    /// <summary>
    /// Returns item.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveI(Item item)
    {
        //if (items.Contains(item) && item.itemAmount >= 1)
        if (items.Contains(item) && items[items.IndexOf(item)].itemAmount > 1) // If item to be removed is in the collection and theres more than 1 instance count..
        {
            //--item.itemAmount;
            // Access the collection via index search and modify item count (decrement).
            items[items.IndexOf(item)].itemAmount--;
        }
        //else if(items.Contains(item) && item.itemAmount <= 1)
        else if (items.Contains(item) && items[items.IndexOf(item)].itemAmount == 1) // If there is only one instance...
        {
            // Remove it from the list.
            items.Remove(item);
        }

        if (onItemChangedCallback != null)
        {
            // Notify listeners that an item change has happened.
            onItemChangedCallback.Invoke();
        }
    }
}
