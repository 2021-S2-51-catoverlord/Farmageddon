using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    public EquipmentType equipType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipType.ToString() + "Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
        {
            return true;
        }

        Equipment equip = item as Equipment;
        return equip != null && equip.equipType == equipType;
    }
}
