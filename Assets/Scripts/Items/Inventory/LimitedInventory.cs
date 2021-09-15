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
        if (items.Count >= NUM_SLOTS)
        {
            Debug.Log("Not enough room");
            return false;
        }

        items.Add(item);

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
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            // Notify listeners that an item change has happened.
            onItemChangedCallback.Invoke();
        }
    }
}
