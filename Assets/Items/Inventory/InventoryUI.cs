using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Limited inventory bar related attributes.
    public GameObject ltdInventoryUI;
    public Transform ltdItemsParent;
    LimitedInventory limitedInventory;
    InventorySlot[] ltdSlots;

    // Main inventory attributes.
    public GameObject inventoryUI;
    public Transform itemsParent;
    Inventory inventory; //Current inv
    InventorySlot[] slots; //List all slots

    // Start is called before the first frame update
    void Start()
    {
        limitedInventory = LimitedInventory.instance;
        limitedInventory.onItemChangedCallback += UpdateUI; // Adds the inventory ui as a listener and what action to do upon being notified.
        //limitedInventoryUI.SetActive(true); // Hides the inventory during the start of gameplay.
        ltdSlots = ltdItemsParent.GetComponentsInChildren<InventorySlot>();

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI; // Adds the inventory ui as a listener and what action to do upon being notified.
        inventoryUI.SetActive(false); // Hides the inventory during the start of gameplay.
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
        // For loop for limited inventory bar.
        for(int i =0; i < ltdSlots.Length; i++)
        {
            if(i < limitedInventory.items.Count)
            {
                ltdSlots[i].AddItem(limitedInventory.items[i]);
            }
            else
            {
                ltdSlots[i].ClearSlot();
            }
        }

        // For loop for main inventory.
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
