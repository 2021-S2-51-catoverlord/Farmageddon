using System.IO;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    [SerializeField] public PlayerController PlayerStats;

    protected string PlayerFullPath;

    private void Awake()
    {
        if(PlayerStats == null)
        {
            PlayerStats = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        PlayerFullPath = Application.persistentDataPath + "/Player.dat";
    }

    /// <summary>
    /// Method to save important player data to file.
    /// </summary>
    public void SavePlayerData()
    {
        PlayerSaveData saveData = GetSaveData();
        FileIO.WriteBinToFile(PlayerFullPath, saveData);
    }

    private PlayerSaveData GetSaveData()
    {
        return new PlayerSaveData(PlayerStats);
    }

    /// <summary>
    /// Method to save current items stored in the equipment inventory to file.
    /// </summary>
    public void LoadPlayerData()
    {
        //string fullPath = BaseSavePath + PlayerFilename + FileExtension;

        PlayerSaveData loadedData = null;

        if(File.Exists(PlayerFullPath))
        {
            loadedData = FileIO.ReadBinFromFile<PlayerSaveData>(PlayerFullPath);
        }

        if(loadedData != null)
        {
            DeserializeAndLoad(loadedData);
            Debug.Log("Player data loaded!.");
        }
        else
        {
            Debug.Log("Player data unavailable.");
        }
    }

    /// <summary>
    /// Helper method to deserialise information and load 
    /// it straight onto the relevant player stats classes.
    /// </summary>
    /// <param name="loadedData"></param>
    private void DeserializeAndLoad(PlayerSaveData loadedData)
    {
        PlayerStats.EntityName =  loadedData.EntityName;
        PlayerStats.Speed = loadedData.Speed;
        PlayerStats.Damage = loadedData.Damage;

        PlayerStats.GetComponent<Transform>().position = new Vector3(loadedData.PosX, loadedData.PosY, loadedData.PosZ);
                                       
        PlayerStats.MaxHP = loadedData.MaxHP;
        PlayerStats.HealthPoints = loadedData.HealthPoints;
        PlayerStats.MaxStamina = loadedData.MaxStamina;
        PlayerStats.StaminaPoints = loadedData.StaminaPoints;
        PlayerStats.Level.experience = loadedData.ExperiencePoints;
        PlayerStats.Level.experienceToNextLevel = loadedData.ExperienceToNextLevel;
        PlayerStats.Level.level = loadedData.Level;
        PlayerStats.Level.UpdateUI();

        PlayerStats.Money.CurrentBalance = loadedData.CurrencyBalance;
        PlayerStats.Money.UpdateUI();
    }   
}