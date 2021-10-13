using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    private const string InventoryFilename = "Inventory";
    private const string EquipmentFilename = "Equipment";

    [SerializeField] public UIManager UIManager;
    [SerializeField] public ItemDatabase ItemDatabase;

    private void Awake()
    {
        if(UIManager == null)
            UIManager = GetComponent<UIManager>();

        if(ItemDatabase == null)
            ItemDatabase = Resources.FindObjectsOfTypeAll<ItemDatabase>()[0];
    }

    /// <summary>
    /// Method to save current items stored in the inventory to file.
    /// </summary>
    public void SaveInventory()
    {
        SaveItems(itemSlots: UIManager.inventory.itemSlots, fileName: InventoryFilename);
    }

    /// <summary>
    /// Method to save current items stored in the equipment inventory to file.
    /// </summary>
    public void SaveEquipment()
    {
        SaveItems(itemSlots: UIManager.equipUI.equipSlots, fileName: EquipmentFilename);
    }

    /// <summary>
    /// Method to save a collection of items to file.
    /// </summary>
    /// <param name="itemSlots"></param>
    /// <param name="fileName"></param>
    private void SaveItems(ItemSlot[] itemSlots, string fileName)
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
    public void LoadInventory()
    {
        ItemSlotSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFilename);

        if(savedSlots == null)
            return;

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

    /// <summary>
    /// Method to load equipment from file and restore gamestate.
    /// </summary>
    public void LoadEquipment()
    {
        ItemSlotSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFilename);

        if(savedSlots == null) // Skip if there is no saved data.
            return;

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