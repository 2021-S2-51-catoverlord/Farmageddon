using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Tool")]
public class Tool : Equipment
{
    public ToolCategory toolCat;
}

public enum ToolCategory
{
    Hoe, Waterpot, Sickle, Axe, Hammer
}