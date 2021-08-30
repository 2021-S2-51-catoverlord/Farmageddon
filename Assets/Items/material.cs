using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Material : Item
{
    private int value;
    public Material(string itemName, int ID, string description, int value) : base(itemName, ID, description)
    {
        this.value = value;
    }
}
