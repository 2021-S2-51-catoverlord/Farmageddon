using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquip;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    void Start()
    {
        inventory = Inventory.instance;

        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquip = new Equipment[equipSlots];
    }

    public void Equip (Equipment newItem)
    {
        int equipIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquip[equipIndex] != null)
        {
            oldItem = currentEquip[equipIndex];
            inventory.AddI(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquip[equipIndex] = newItem;
    }

    public void Unequip (int equipIndex)
    {
        if (currentEquip[equipIndex] != null)
        {
            Equipment oldItem = currentEquip[equipIndex];
            inventory.AddI(oldItem);

            currentEquip[equipIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquip.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
