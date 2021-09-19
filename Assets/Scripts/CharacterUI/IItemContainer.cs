using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();
}
