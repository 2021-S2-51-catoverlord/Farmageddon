using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public Button removeButton;

    Item item;

    /// <summary>
    /// Add item to inv slot
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    /// <summary>
    /// Clear the inv slot
    /// </summary>
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
    }

    public void OnRemoveButton()
    {
        if(GetComponent<InventorySlot>().tag == "Limited Inventory")
        {
            LimitedInventory.instance.RemoveI(item);
        }
        else if(GetComponent<InventorySlot>().tag == "Inventory")
        { 
            Inventory.instance.RemoveI(item);
        }
    }
}
