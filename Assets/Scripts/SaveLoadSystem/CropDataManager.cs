using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CropDataManager : MonoBehaviour, ISaveable
{
    protected string CropDataPath;
    public TileController crops;

    public void Awake()
    {
        CropDataPath = $"{Application.persistentDataPath}/Crops.dat";

        Debug.Log(CropDataPath);

        crops = GetComponent<TileController>();
    }

    public void SaveData()
    {
        CropSaveData saveData = new CropSaveData(crops);

        FileIO.WriteBinToFile(CropDataPath, saveData);

        Debug.Log($"Crop data saved to: {CropDataPath}");
    }

    public void LoadData()
    {
        CropSaveData loadedData = null;

        if (File.Exists(CropDataPath))
        {
            loadedData = FileIO.ReadBinFromFile<CropSaveData>(CropDataPath);
        }

        if(loadedData != null)
        {
            Debug.Log("Clearing Farmland");
            crops.ClearAllFarmland();
            ReconstructCropData(loadedData);
            Debug.Log($"Crop data loaded from: {CropDataPath}");
        }
        else
        {
            Debug.Log("TileController's saved data is currently unavailable.");
        }
    }

    public void ReconstructCropData(CropSaveData data)
    {
        foreach(IndividualCrop crop in data.savedCrops)
        {
            crops.PlaceTile(crop.TilePosition, crop.TileName, crop.growthStage);
        }
    }
}
