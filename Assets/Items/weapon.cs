using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Equipment
{
    public Weapon(string itemName, int ID, string description) : base(itemName, ID, description)
    {

    }
}