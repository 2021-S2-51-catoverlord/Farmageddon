using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    private const string InventorySavePath = "Inventory";
    private const string EquipmentSavePath = "Equipment";

	[SerializeField] public UIManager UIManager;
	[SerializeField] public ItemDatabase ItemDatabase;

    private void Start()
    {
		if(UIManager == null)
			UIManager = GameObject.Find("CharacterUI").GetComponent<UIManager>();

		if(ItemDatabase == null)
			ItemDatabase = Resources.FindObjectsOfTypeAll<ItemDatabase>()[0];
	}

    public void SaveInventory()
	{
		SaveItems(itemSlots: UIManager.Inventory.ItemSlots, fileName: InventorySavePath);
	}

	public void SaveEquipment()
	{
		SaveItems(itemSlots: UIManager.EquipUI.EquipSlots, fileName: EquipmentSavePath);
	}

	private void SaveItems(ItemSlot[] itemSlots, string fileName)
	{
		var saveData = new ItemSlotSaveData(numItems: itemSlots.Length);

		for(int i = 0; i < saveData.SavedSlots.Length; i++)
		{
			ItemSlot itemSlot = itemSlots[i];

			if(itemSlot.Item == null)
			{
				saveData.SavedSlots[i] = null;
			}
			else
            {
                saveData.SavedSlots[i] = new ItemObjSaveData(id: itemSlot.Item.Id, amount: itemSlot.Amount);
			}
        }

		ItemSaveIO.SaveItems(items: saveData, fileName: fileName);
	}

	public void LoadInventory()
	{
		ItemSlotSaveData savedSlots = ItemSaveIO.LoadItems(InventorySavePath);
		
		if(savedSlots == null) 
			return;
		
		UIManager.Inventory.Clear();

		for(int i = 0; i < savedSlots.SavedSlots.Length; i++)
		{
			ItemSlot itemSlot = UIManager.Inventory.ItemSlots[i];
			ItemObjSaveData savedSlot = savedSlots.SavedSlots[i];

			if(savedSlot == null) // Skip empty slots.
			{
				itemSlot.Item = null;
				itemSlot.Amount = 0;
			}
			else
			{
				itemSlot.Item = ItemDatabase.GetItem(savedSlot.ItemID);
				itemSlot.Amount = savedSlot.Amount;
			}
		}
	}

	public void LoadEquipment()
	{
		ItemSlotSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentSavePath);
		
		if(savedSlots == null) // Skip if there is no saved data.
			return;

		UIManager.EquipUI.Clear();

		foreach(ItemObjSaveData savedSlot in savedSlots.SavedSlots) // For each Item saved data.
		{
			if(savedSlot == null) // Skip empty slots.
			{
				continue;
			}

			Item item = ItemDatabase.GetItem(savedSlot.ItemID);
            UIManager.EquipUI.AddItem((Equipment)item, out Equipment oldItem); 
        }
	}
}