using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickaxe", menuName = "Inventory/Equipment/Pickaxe")]
public class Pickaxe : Equipment
{
    public Pickaxe(string itemName, int ID, string description) : base(itemName, ID, description)
    {

    }
}