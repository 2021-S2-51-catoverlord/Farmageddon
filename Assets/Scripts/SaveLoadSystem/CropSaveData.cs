using Gameplay;
using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IndividualCrop
{
    public Vector3 TilePosition;
    public string TileName;
    public int growthStage;

    public IndividualCrop(Vector3 pos, String name, int stage)
    {
        this.TilePosition = pos;
        this.TileName = name;
        this.growthStage = stage;
    }
}

[Serializable]
public class CropSaveData
{
    public List<IndividualCrop> savedCrops;


    public CropSaveData(TileController crops)
    {
        savedCrops = new List<IndividualCrop>();
        Regex crop_name = new Regex("^.*?(?=_)");
        Regex growth_stage = new Regex("([^_])+$");

        foreach (KeyValuePair<Vector3, IGameTile> crop in crops.tiles)
        {
            Vector3 vector = crop.Key;
            String cropDesc = crop.Value.TileBase.name;

            String cropType = crop_name.Match(cropDesc).Value;


            if (cropType.Equals("beet"))
            {
                cropType = "beetroot";
            }

            int growthStage = int.Parse(growth_stage.Match(cropDesc).Value); 
            growthStage -= 1;

            Debug.Log("Saved crop of type " + cropType + " at growth stage " + growthStage);

            savedCrops.Add(new IndividualCrop(vector, cropType, growthStage));
        }
    }
}
