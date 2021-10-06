using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    public Item item;
    [SerializeField]
    private Item[] requiredItem;
    [SerializeField]
    private int[] quantityRequired;

    public Item[] RequiredItem { get => requiredItem; set => requiredItem = value; }
    public int[] QuantityRequired { get => quantityRequired; set => quantityRequired = value; }
}
