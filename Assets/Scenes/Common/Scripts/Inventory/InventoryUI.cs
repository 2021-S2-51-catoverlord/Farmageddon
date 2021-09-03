using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory; //Current inv
    InventorySlot[] slots; //List all slots

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        // Adds the inventory ui as a listener and what action to do upon being notified.
        inventory.onItemChangedCallback += UpdateUI;
        
        // Hides the inventory during the start of gameplay.
        inventoryUI.SetActive(false);

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    /// <summary>
    /// Updates UI:
    ///     - Adds items
    ///     - Clear empty slots
    /// </summary>
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) //loop through slots
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
