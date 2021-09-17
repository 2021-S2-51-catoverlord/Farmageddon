using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    // Chest inventory attributes.
    public GameObject chestInventoryUI;
    public Transform chestParent;
    ChestInventory inventory; //Current inv
    InventorySlot[] chestSlots; //List all slots

    // Start is called before the first frame update
    void Start()
    {
        inventory = ChestInventory.instance;
        inventory.onItemChangedCallback += UpdateUI; // Adds the inventory ui as a listener and what action to do upon being notified.
        chestInventoryUI.SetActive(false); // Hides the inventory during the start of gameplay.
        chestSlots = chestParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        // For loop for limited inventory bar.
        for (int i = 0; i < chestSlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                chestSlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                chestSlots[i].ClearSlot();
            }
        }
    }
}
