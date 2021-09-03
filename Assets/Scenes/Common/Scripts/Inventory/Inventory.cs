using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    #endregion

    public static int NUM_SLOTS = 36; //Slots in inv
    public List<Item> items = new List<Item>(); //Current list of items in inv

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

        return true;
    }
    /// <summary>
    /// Returns item.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveI(Item item)
    {
        items.Remove(item);
    }
}
