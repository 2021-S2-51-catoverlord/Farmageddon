using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armour", menuName = "Inventory/Equipment/Armor")]
public class Armor : Equipment
{
    public Armor(string itemName, int ID, string description) : base(itemName, ID, description)
    {

    }
}
