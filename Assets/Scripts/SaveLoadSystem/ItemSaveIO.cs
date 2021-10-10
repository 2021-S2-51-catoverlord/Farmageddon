using System.IO;
using UnityEngine;

public static class ItemSaveIO
{
    public static readonly string BaseSavePath;
    public static readonly string FileExtension;

    static ItemSaveIO()
    {
        BaseSavePath = Application.persistentDataPath + "/";
        FileExtension = ".dat";
    }

    /// <summary>
    /// Method to save current game's inventory/equipUI objects to file.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="fileName"></param>
    public static void SaveItems(ItemSlotSaveData items, string fileName)
    {
        string fullPath = BaseSavePath + fileName + FileExtension;
        //Debug.Log(fullPath);
        FileIO.WriteBinToFile(fullPath, items);
    }

    /// <summary>
    /// Method to load saved inventory/equipUI objects from file
    /// and return it as a SaveData object.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static ItemSlotSaveData LoadItems(string fileName)
    {
        string fullPath = BaseSavePath + fileName + FileExtension;

        if(File.Exists(fullPath))
        {
            return FileIO.ReadBinFromFile<ItemSlotSaveData>(fullPath);
        }
        return null;
    }
}
