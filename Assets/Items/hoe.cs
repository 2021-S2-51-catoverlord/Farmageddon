using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hoe", menuName = "Inventory/Equipment/Hoe")]
public class Hoe : Equipment
{
    public Hoe(string itemName, int ID, string description) : base(itemName, ID, description)
    {

    }
}
