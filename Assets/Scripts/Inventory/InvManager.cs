using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipUI equipUI;
    [SerializeField] Image draggableItem;
    private ItemSlot draggedSlot;

    private void Awake()
    {
        //Right Click Events
        inventory.OnRightClickEvent += Equip;
        equipUI.OnRightClickEvent += Unequip;
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
    }

    private void Equip(ItemSlot itemSlot)
    {
        Equipment equipment = itemSlot.Item as Equipment;
        if (equipment != null)
        {
            Equip(equipment);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        Equipment equipment = itemSlot.Item as Equipment;
        if (equipment != null)
        {
            Unequip(equipment);
        }
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
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
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if(dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            Equipment dragItem = draggedSlot.Item as Equipment;
            Equipment dropItem = dropItemSlot.Item as Equipment;

            if(draggedSlot is EquipSlot)
            {
                if (dragItem != null)
                {
                    
                }
            }

            if (dropItem is EquipSlot)
            {

            }

            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }

    /// <summary>
    /// Equip equipment
    /// </summary>
    /// <param name="equipment"></param>
    public void Equip (Equipment equipment)
    {
        if (inventory.RemoveItem(equipment)) //Remove item from inventory
        {
            Equipment oldItem;
            if (equipUI.AddItem(equipment, out oldItem)) //Add to panel
            {
                if (oldItem != null) //Return item to inventory if there is a previous item
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
    public void Unequip (Equipment equipment)
    {
        if (!inventory.IsFull() && equipUI.RemoveItem(equipment))
        {
            inventory.AddItem(equipment);
        }
    }
}
