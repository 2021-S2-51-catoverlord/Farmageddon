using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public Equipment(string itemName, int ID, string description) : base(itemName, ID, description)
    {

    }
}