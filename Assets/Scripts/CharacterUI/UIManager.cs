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
        if(itemSlot.Item is Equipment)
        {
            Equip((Equipment)itemSlot.Item);
        }
        else if(itemSlot.Item is Food)
        {
            Food food = (Food)itemSlot.Item;

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
        if(itemSlot.Item is Equipment)
        {
            Unequip((Equipment)itemSlot.Item);
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
        int addableStack = dropItemSlot.Item.MaxStacks - dropItemSlot.Amount;
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
            Equipment oldItem;
            if(equipUI.AddItem(equipment, out oldItem)) //Add to panel
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
        Debug.Log("Saved inventory as binary.");
    }

    public void LoadInventoryData()
    {
        saveManager.LoadInventory();
        saveManager.LoadEquipment();
        Debug.Log("Loaded inventory from binary.");
    }
}
