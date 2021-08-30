using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class item : ScriptableObject
{
    private string itemName = "uninported item";
    private int ID;
    private string description;
    private int price;


    public item(string itemName, int ID, string description)
    {
        this.itemName = itemName;
        this.ID = ID;
        this.description = description;
    }

}
