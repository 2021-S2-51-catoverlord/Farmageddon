/*
 * This class contains the scriptable object for the seed item,
 * which encapsulates the following methods:
 * Data:
 * - Seed growth time in days (growthTime)
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Seed")]
public class Seed : Item
{
    public int growthTime = 0;
}