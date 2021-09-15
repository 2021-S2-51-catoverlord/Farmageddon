using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Equipment/Tool")]
public class Tool : Equipment
{
    public ToolCategory toolCat;
    public int toolAtk;
}

public enum ToolCategory
{
    Hoe, Waterpot, Sickle, Axe, Hammer
}