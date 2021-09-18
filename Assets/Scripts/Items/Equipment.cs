using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipType;

    public int defence;
    public int attack;
}

public enum EquipmentType
{
    Head, Chest, Weapon, Legs, Feet, Tool
}
