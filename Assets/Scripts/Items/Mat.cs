/*
 * This class contains the scriptable object for the item materials,
 * which encapsulates the following methods:
 * Data:
 * - Enumerations for material category (materialCat)
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
public class Mat : Item
{
    public MaterialCategory materialCat;
}

public enum MaterialCategory
{
    Minerals, Jewels, Sticks, Stones, Grass, Food
}
