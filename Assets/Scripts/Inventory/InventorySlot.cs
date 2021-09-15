using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Text stack;

    Item item;

    /// <summary>
    /// Add item to inv slot
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        if(newItem.itemAmount > 1) // If the item's stack count is more than 1...
        {
            // Display a text.
            stack.text = newItem.itemAmount.ToString();
        }
        else // If it is 1...
        {
            // Make the text "invisible".
            stack.text = "";
        }
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

    public void OnRemoveButton()
    {
        if (GetComponent<InventorySlot>().tag == "Limited Inventory")
        {
            LimitedInventory.instance.RemoveI(item);
        }
        else if (GetComponent<InventorySlot>().tag == "Inventory")
        {
            Inventory.instance.RemoveI(item);
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
