using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour, IItemContainer
{
    [SerializeField] Item[] startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }

        SetStartingItems();
    }

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        SetStartingItems();
    }

    public void SetStartingItems()
    {
        Clear();

        for (int i = 0; i < startingItems.Length; i++) //stack if existing
        {
            AddItem(startingItems[i].GetItemCopy());
        }
    }

    /// <summary>
    /// Add item to the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Remove item from the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;
                if (itemSlots[i].Amount == 0)
                {
                    itemSlots[i].Item = null;
                }
                return true;
            }
        }

        return false;
    }

    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.Id == itemID)
            {
                itemSlots[i].Amount--;
                if (itemSlots[i].Amount == 0)
                {
                    itemSlots[i].Item = null;
                }
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// Check the inventory slots are full
    /// </summary>
    /// <returns></returns>
    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }

        return true;
    }

    public int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.Id == itemID)
            {
                number += itemSlots[i].Amount;
            }
        }

        return number;
    }

    public void Clear()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
}