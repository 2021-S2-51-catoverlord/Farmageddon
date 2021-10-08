using UnityEngine;

public static class ItemSaveIO
{
    private static readonly string BaseSavePath;

    static ItemSaveIO()
    {
        BaseSavePath = Application.persistentDataPath;
    }

    /// <summary>
    /// Method to save current game's inventory/equipUI objects to file.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="fileName"></param>
    public static void SaveItems(ItemSlotSaveData items, string fileName)
    {
        FileReadWrite.WriteBinToFile(BaseSavePath + "/" + fileName + ".dat", items);
    }

    /// <summary>
    /// Method to load saved inventory/equipUI objects from file
    /// and return it as a SaveData object.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static ItemSlotSaveData LoadItems(string fileName)
    {
        string filePath = BaseSavePath + "/" + fileName + ".dat";

        if(System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadBinFromFile<ItemSlotSaveData>(filePath);
        }
        return null;
    }
}
