using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsParentScript : MonoBehaviour
{
    private static int NUM_SLOTS = 36;
    
    [SerializeField]
    private GameObject inventorySlotPrefab;

    private List<InventorySlotScript> slots = new List<InventorySlotScript>();

    // Start is called before the first frame update
    void Start()
    {
        AddSlots();
    }

    /// <summary>
    /// Creates the individual slots for the inventory.
    /// </summary>
    public void AddSlots()
    {
        // Create individual items slots. 
        for(int i = 0; i < NUM_SLOTS; i++)
        {
            InventorySlotScript slot = Instantiate(inventorySlotPrefab, transform).GetComponent<InventorySlotScript>();
            slots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        // For each slot in the items parent..
        foreach(InventorySlotScript slot in slots)
        {
            // If the slot is empty...
            if(slot.IsEmpty) 
            {
                // Add the item to the slot.
                slot.AddItem(item);
                return true;
            }
        }

        // If the inventory is full...return false to caller.
        return false;
    }
}
