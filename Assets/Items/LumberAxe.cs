using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/LumberAxe")]
public class LumberAxe : Equipment
{
    public LumberAxe(string itemName, int ID, string description) : base(itemName, ID, description)
    {

    }
}