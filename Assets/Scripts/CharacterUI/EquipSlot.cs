/*
 * This class contains the equipment slots for the equipment UI,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - OnValidate Method
 * - CanReceiveItem: inheritable from ItemSlots, allows the inventory to receive the
 *      item and checks the equipment type is the same as equipment slot type.
 */

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
        if(item == null)
        {
            return true;
        }

        Equipment equip = item as Equipment;
        return equip != null && equip.equipType == equipType;
    }
}
