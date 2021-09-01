using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mat", menuName = "Inventory/Mat")]
public abstract class Mat : Item
{
    private int value;
    public Mat(string itemName, int ID, string description, int value) : base(itemName, ID, description)
    {
        this.value = value;
    }
}