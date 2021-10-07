/*
 * This class contains the scriptable object for Items,
 * which encapsulates the following methods:
 * Data:
 * - Strings for item name and description (itemName, itemDescription)
 * - Sprite of the item (icon)
 * - Item price (price)
 * - Max stacks and item can have, range from 1-99 (maxStacks)
 * 
 * Methods:
 * - On Validate method to generate IDs
 * - Getter for ID (ID)
 * - Get item copy (GetItemCopy)
 * - Destroy item (Destroy)
 */

using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string id;
    public string itemName;
    public Sprite icon;
    public string itemDescription;
    public int price = 0;
    [Range(1, 99)] 
    public int maxStacks = 1;

    public string ID
    {
        get
        {
            return id;
        }
    }

    private void OnValidate()
    {
        //Generates ids
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Item GetItemCopy()
    {
        return this; //reference to object
    }

    public virtual void Destroy()
    {

    }
}