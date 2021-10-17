using UnityEngine;

public class ItemSaveManager : MonoBehaviour, ISaveable
{
    protected const string InventoryFilename = "Inventory";
    protected const string EquipmentFilename = "Equipment";

    [SerializeField] public UIManager UIManager;
    [SerializeField] public ItemDatabase ItemDatabase;

    public void Awake()
    {
        if(UIManager == null)
        {
            UIManager = GetComponent<UIManager>();
        }

        if(ItemDatabase == null)
        {
            ItemDatabase = Resources.FindObjectsOfTypeAll<ItemDatabase>()[0];
        }
    }

    /// <summary>
    /// Method to save current items stored in the inventory
    /// and equipUI to file.
    /// </summary>
    public void SaveData()
    {
        SaveSlotData(itemSlots: UIManager.inventory.itemSlots, fileName: InventoryFilename);
        SaveSlotData(itemSlots: UIManager.equipUI.equipSlots, fileName: EquipmentFilename);

        Debug.Log("Inventory and Equipments saved to: " + Application.persistentDataPath + "/Inventory.dat & Equipment.dat");
    }

    /// <summary>
    /// Method to load saved items for the inventory
    /// and equipUI from a save file.
    /// </summary>
    public void LoadData()
    {
        LoadInventory();
        LoadEquipment();

        Debug.Log("Inventory and Equipments loaded from: " + Application.persistentDataPath + "/Inventory.dat & Equipment.dat");
    }

    /// <summary>
    /// Method to save a collection of items to file.
    /// </summary>
    /// <param name="itemSlots"></param>
    /// <param name="fileName"></param>
    private void SaveSlotData(ItemSlot[] itemSlots, string fileName)
    {
        var saveData = new ItemSlotSaveData(numItems: itemSlots.Length);

        for(int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = itemSlots[i];

            if(itemSlot.Item == null) // Assign empty slots.
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new ItemObjSaveData(id: itemSlot.Item.ID, amount: itemSlot.Amount);
            }
        }

        ItemSaveIO.SaveItems(items: saveData, fileName: fileName);
    }

    /// <summary>
    /// Method to load inventory from file and restore gamestate.
    /// </summary>
    private void LoadInventory()
    {
        ItemSlotSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFilename);

        if(savedSlots != null)
        {
            UIManager.inventory.Clear();

            for(int i = 0; i < savedSlots.SavedSlots.Length; i++)
            {
                ItemSlot itemSlot = UIManager.inventory.itemSlots[i];
                ItemObjSaveData savedSlot = savedSlots.SavedSlots[i];

                if(savedSlot == null) // Assign empty slots.
                {
                    itemSlot.Item = null;
                    itemSlot.Amount = 0;
                }
                else
                {
                    itemSlot.Item = ItemDatabase.GetItemCopy(savedSlot.ItemID);
                    itemSlot.Amount = savedSlot.Amount;
                }
            }
        }
    }

    /// <summary>
    /// Method to load equipment from file and restore gamestate.
    /// </summary>
    private void LoadEquipment()
    {
        ItemSlotSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFilename);

        if(savedSlots != null) // Skip if there is no saved data.
        {
            UIManager.equipUI.Clear();

            for(int i = 0; i < savedSlots.SavedSlots.Length; i++) // For each Item saved data.
            {
                EquipSlot equipSlot = UIManager.equipUI.equipSlots[i];
                ItemObjSaveData savedSlot = savedSlots.SavedSlots[i];

                if(savedSlot == null) // Skip empty slots.
                {
                    equipSlot.Item = null;
                    equipSlot.Amount = 0;
                }
                else
                {
                    equipSlot.Item = ItemDatabase.GetItemCopy(savedSlot.ItemID);
                    equipSlot.Amount = savedSlot.Amount;
                }
            }
        }
    }
}