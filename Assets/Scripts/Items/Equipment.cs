/*
 * This class contains the scriptable object for equipment items,
 * which encapsulates the following methods:
 * Data:
 * - Enumeration for the equipment type (equipType)
 * - Defence amount (defence)
 * - Attack amount (attack)
 * 
 * Methods:
 * - Get item copy (GetItemCopy)
 * - Destroy item (Destroy)
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipType;

    public int defence;
    public int attack;

    public override Item GetItemCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }
}

public enum EquipmentType
{
    Head, Chest, Weapon, Legs, Feet, Tool
}
