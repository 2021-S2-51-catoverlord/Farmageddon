using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
public class Mat : Item
{
    public MaterialCategory materialCat;

    public enum MaterialCategory
    {
        Minerals, Jewels, Sticks, Stones, Grass
    }
}
