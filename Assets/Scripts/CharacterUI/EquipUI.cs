/*
 * This class contains the equipmentUI, which encapsulates the following methods:
 * 
 * Methods:
 * - Awake Method
 * - OnValidate Method
 * - AddItem: Loop through find slot that can hold equipment, then equip.
 * - RemoveItem: Assign null to slot.
 */

using System;
using UnityEngine;

public class EquipUI : MonoBehaviour
{
    [SerializeField] Transform equipSlotParent;
    [SerializeField] public EquipSlot[] equipSlots;

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Awake()
    {
        for(int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipSlots[i].OnDragEvent += OnDragEvent;
            equipSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if(equipSlotParent != null)
        {
            equipSlots = equipSlotParent.GetComponentsInChildren<EquipSlot>();
        }
    }

    /// <summary>
    /// Add item to equipment slot
    /// </summary>
    /// <param name="equipment"></param>
    /// <returns></returns>
    public bool AddItem(Equipment equipment, out Equipment oldItem)
    {
        for(int i = 0; i < equipSlots.Length; i++)
        {
            if(equipSlots[i].equipType == equipment.equipType)
            {
                oldItem = (Equipment)equipSlots[i].Item;
                equipSlots[i].Item = equipment;
                equipSlots[i].Amount = 1;
                return true;
            }
        }

        oldItem = null;
        return false;
    }

    /// <summary>
    /// Remove item from equipment slot
    /// </summary>
    /// <param name="equipment"></param>
    /// <returns></returns>
    public bool RemoveItem(Equipment equipment)
    {
        for(int i = 0; i < equipSlots.Length; i++)
        {
            if(equipSlots[i].Item == equipment)
            {
                equipSlots[i].Item = null;
                equipSlots[i].Amount = 0;
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        for(int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].Item = null;
            equipSlots[i].Amount = 0;
        }
    }
}
