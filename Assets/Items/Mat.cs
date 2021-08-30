using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mat : Item
{
    private int value;
    public Mat(string itemName, int ID, string description, int value) : base(itemName, ID, description)
    {
        this.value = value;
    }
}