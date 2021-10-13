/*
 * This class contains the scriptable object for food items,
 * which encapsulates the following methods:
 * Data:
 * - A boolean for consumables (isConsumable)
 * - Amount that the food heals (healHealth)
 * 
 * Methods:
 * - Get item copy (GetItemCopy)
 * - Destroy item (Destroy)
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    public bool isConsumable;
    public int healHeath;

    public override Item GetItemCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }
}
