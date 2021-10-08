using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string id;
    public string itemName;
    public Sprite icon;
    public int price = 0;
    [Range(1, 99)] 
    public int MaxStacks = 1;

    public string Id
    {
        get
        {
            return id;
        }
    }

#if UNITY_EDITOR
    protected void OnValidate()
    {
        //Generates ids
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual Item GetItemCopy()
    {
        return this; //reference to object
    }

    public virtual void Destroy()
    {

    }
}