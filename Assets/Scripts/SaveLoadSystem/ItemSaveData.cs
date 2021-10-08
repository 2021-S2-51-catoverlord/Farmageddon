using System;

[Serializable]
public class ItemObjSaveData
{
    public string ItemID;
    public int Amount;

    public ItemObjSaveData(string id, int amount)
    {
        ItemID = id;
        Amount = amount;
    }
}

[Serializable]
public class ItemSlotSaveData
{
    public ItemObjSaveData[] SavedSlots;

    public ItemSlotSaveData(int numItems)
    {
        SavedSlots = new ItemObjSaveData[numItems];
    }
}
