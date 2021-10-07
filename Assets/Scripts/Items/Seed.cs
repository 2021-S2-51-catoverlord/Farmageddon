using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Seed")]
public class Seed : Item
{
    public int growthTime = 0; // In days.
}