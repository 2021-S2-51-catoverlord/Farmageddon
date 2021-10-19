using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CropSaveManager : MonoBehaviour, ISaveable
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
        FileIO.WriteBinToFile(CropDataPath, new CropSaveData(crops));

        Debug.Log($"Crop data saved to: {CropDataPath}");
    }

    public void LoadData()
    {
        CropSaveData loadedData = null;
        //List<IndividualCrop> loadedData = new List<IndividualCrop>();

        if (File.Exists(CropDataPath))
        {
            loadedData = FileIO.ReadBinFromFile<CropSaveData>(CropDataPath);
        }
        //streamIn, PrefixStyle.Base128, Serializer.ListItemTag

        if (loadedData != null)
        {
            Debug.Log("Clearing Farmland");
            crops.ClearAllFarmland();
            ReconstructCropData(loadedData.savedCrops);
            Debug.Log($"Crop data loaded from: {CropDataPath}");
        }
        else
        {
            Debug.Log("TileController's saved data is currently unavailable.");
        }
    }

    public void ReconstructCropData(List<IndividualCrop> data)
    {
        //foreach(IndividualCrop crop in data.savedCrops)
        //{
        //    crops.PlaceTile(crop.TilePosition, crop.TileName, crop.growthStage);
        //}
        IList list = data;
        for(int i = 0; i < list.Count; i++)
        {
            IndividualCrop crop = (IndividualCrop)list[i];
            crops.PlaceTile(crop.TilePosition, crop.TileName, crop.growthStage);
        }
    }
}
