using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] public Item[] Items;

	/// <summary>
	/// Method to do a search through the list of items in the 
	/// database (created from searching the game folder) and match it 
	/// with an ID passed in.
	/// </summary>
	/// <param name="itemID"></param>
	/// <returns></returns>
	public Item GetItem(string itemID)
	{
		foreach(Item item in Items)
		{
			if(item.Id == itemID) // Check if item ID is found in the database.
			{
				return item.GetItemCopy(); // Return a copy of the item.
			}
		}
		return null;
	}

#if UNITY_EDITOR
    private void OnValidate()
    {
		LoadItemAssets();
    }

    private void OnEnable()
	{
		EditorApplication.projectChanged += LoadItemAssets;
	}

	private void OnDisable()
	{
		EditorApplication.projectChanged -= LoadItemAssets;
	}

	private void LoadItemAssets()
	{
		Items = FindAssetsByType<Item>();
	}

	/// <summary>
	/// Method to find all assets of type T in the game folder 
	/// and return it as an array of type T.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T[] FindAssetsByType<T>() where T : Object
	{
		string type = typeof(T).ToString().Replace("UnityEngine.", "");

		string[] guids = AssetDatabase.FindAssets("t:" + type);

		T[] assets = new T[guids.Length];

		for(int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
		}
		return assets;
	}
#endif
}
