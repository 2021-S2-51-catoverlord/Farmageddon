/*
 * This class contains the UI Manager for the inventory and equipment UI,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - Awake Method
 * - InventoryRightClick: Upon right click, equip the item if the item is an Equipment, 
 *      or consume the item and heal if the item is Food.
 * - EquipmentRightClick: Unequip the equipment.
 * - BeginDrag: Set item so that it doesn't get dragged underneath the other slots.
 * - EndDrag: End the drag
 * - Drag: Set item position to mouse position
 * - Drop: Drop item into the slot where the drag ends.
 * - DropOutside: Destroy the item if the item is dropped outside of the inventory
 *      or equipment UI.
 * - SwapItems: Swap items between where drag begun and where drag ends.
 * - AddStacks: Add stacks until item is full.
 * - Equip: Remove item from inventory and add to the equipment UI. If something is already 
 *      in the slot, return the previous item to the innventory.
 * - Unequip: Remove item from the equipment UI and return it to the inventory.
 */

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] public EquipUI equipUI;
    [SerializeField] Image draggableItem;
    [SerializeField] DropArea dropArea;
    [Space]
    [SerializeField] PlayerController player;
    [SerializeField] ItemSaveManager saveManager;

    private ItemSlot draggedSlot;

    private void Awake()
    {
        // Find the player gameobject and get reference to its script if it's not assigned yet.
        if(player == null)
            player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Find the item saver gameobject and get reference to its script if it's not assigned yet.
        if(saveManager == null)
            saveManager = GetComponent<ItemSaveManager>();

        //Right Click Events
        inventory.OnRightClickEvent += InventoryRightClick;
        equipUI.OnRightClickEvent += EquipmentRightClick;
        //Begin Drag Events
        inventory.OnBeginDragEvent += BeginDrag;
        equipUI.OnBeginDragEvent += BeginDrag;
        //End Drag Events
        inventory.OnEndDragEvent += EndDrag;
        equipUI.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        equipUI.OnDragEvent += Drag;
        //Drop Event
        inventory.OnDropEvent += Drop;
        equipUI.OnDropEvent += Drop;
        dropArea.OnDropEvent += DropOutside;
    }

    private void InventoryRightClick(ItemSlot itemSlot)
    {
        if(itemSlot.Item is Equipment equipment)
        {
            Equip(equipment);
        }
        else if(itemSlot.Item is Food food)
        {
            if(food.IsConsumable)
            {
                player.Heal(food.healHeath); // Perform healing.
                // Tells the health bar to update according to the player's current hp.
                GameObject.Find("Health Bar").GetComponent<StatBarController>().SetCurrentValue(player.HealthPoints);
                inventory.RemoveItem(food);
                food.Destroy();
            }
        }
    }

    private void EquipmentRightClick(ItemSlot itemSlot)
    {
        if(itemSlot.Item is Equipment equipment)
        {
            Unequip(equipment);
        }
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if(draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if(draggedSlot == null)
        {
            return;
        }

        if(dropItemSlot.CanAddStack(draggedSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if(dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }

    private void DropOutside()
    {
        if(draggedSlot == null)
        {
            return;
        }

        draggedSlot.Item.Destroy();
        draggedSlot.Item = null;
        draggedSlot.Amount = 0;
    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        Item draggedItem = draggedSlot.Item;
        int draggedAmount = draggedSlot.Amount;

        draggedSlot.Item = dropItemSlot.Item;
        draggedSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedAmount;
    }

    private void AddStacks(ItemSlot dropItemSlot)
    {
        int addableStack = dropItemSlot.Item.maxStacks - dropItemSlot.Amount;
        int stackAdd = Mathf.Min(addableStack, draggedSlot.Amount);

        dropItemSlot.Amount += stackAdd;
        draggedSlot.Amount -= stackAdd;
    }

    /// <summary>
    /// Equip equipment
    /// </summary>
    /// <param name="equipment"></param>
    public void Equip(Equipment equipment)
    {
        if(inventory.RemoveItem(equipment)) //Remove item from inventory
        {
            if(equipUI.AddItem(equipment, out Equipment oldItem)) //Add to panel
            {
                if(oldItem != null) //Return item to inventory if there is a previous item
                {
                    inventory.AddItem(oldItem);
                }
            }
            else
            {
                inventory.AddItem(equipment);
            }
        }
    }

    /// <summary>
    /// Unequip 
    /// </summary>
    /// <param name="equipment"></param>
    public void Unequip(Equipment equipment)
    {
        if(!inventory.IsFull() && equipUI.RemoveItem(equipment))
        {
            inventory.AddItem(equipment);
        }
    }

    public void SaveInventoryData()
    {
        saveManager.SaveInventory();
        saveManager.SaveEquipment();
        Debug.Log("Inventory and Equipments saved to: " + Application.persistentDataPath);
    }

    public void LoadInventoryData()
    {
        saveManager.LoadInventory();
        saveManager.LoadEquipment();
        Debug.Log("Inventory and Equipments loaded from: " + Application.persistentDataPath);
    }
}
