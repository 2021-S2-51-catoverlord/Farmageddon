using System.IO;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    //public static readonly string BaseSavePath = Application.persistentDataPath + "/";
    //public static readonly string FileExtension = ".dat";
    //protected const string PlayerFilename = "Player";
    public static readonly string PlayerFullPath = Application.persistentDataPath + "/" + "Player" + ".dat";

    [SerializeField] public PlayerController PlayerStats;
    [SerializeField] public LevelSystem PlayerLevel;
    //[SerializeField] public MoneySystem PlayerMoney;

    private void Awake()
    {
        if(PlayerStats == null || PlayerLevel == null)
        {
            PlayerStats = GetComponent<PlayerController>();
            PlayerLevel = GameObject.FindObjectOfType<LevelSystem>();
            //PlayerMoney = GameObject.FindObjectOfType<MoneySystem>();
        }
    }

    /// <summary>
    /// Method to save important player data to file.
    /// </summary>
    public void SavePlayerData()
    {
        var saveData = new PlayerSaveData(PlayerStats, PlayerLevel);
        //string fullPath = BaseSavePath + PlayerFilename + FileExtension;
        FileIO.WriteBinToFile(PlayerFullPath, saveData);
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

        //PlayerStats.ExperiencePoints = loadedData.ExperiencePoints;
        //PlayerStats.ExperienceToNextLevel = loadedData.ExperienceToNextLevel;
        //PlayerStats.Level = loadedData.Level;

        PlayerLevel.experience = loadedData.ExperiencePoints;
        PlayerLevel.experienceToNextLevel = loadedData.ExperienceToNextLevel;
        PlayerLevel.level = loadedData.Level;

        //PlayerMoney.currentBalance = loadedData.CurrencyBalance;
    }   
}
