using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Seed")]
public class Seed : Item
{
    public int growthTime = 0; // In days.
}