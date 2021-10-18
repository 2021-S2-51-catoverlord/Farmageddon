using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour, ISaveable
{
    protected string PlayerDataPath;
    public PlayerController Player;

    public void Awake()
    {
        PlayerDataPath = $"{Application.persistentDataPath}/Player.dat";

        if(Player == null)
        {
            Player = GetComponent<PlayerController>();
        }
    }

    /// <summary>
    /// Method to save important player data to file.
    /// </summary>
    public void SaveData()
    {
        FileIO.WriteBinToFile(PlayerDataPath, new PlayerSaveData(Player));

        Debug.Log($"Player data saved to: {PlayerDataPath}");
    }

    /// <summary>
    /// Method to load player data stored in the player file 
    /// onto player and its associated classes.
    /// </summary>
    public void LoadData()
    {
        PlayerSaveData loadedData = null;

        if(File.Exists(PlayerDataPath))
        {
            loadedData = FileIO.ReadBinFromFile<PlayerSaveData>(PlayerDataPath);
        }

        if(loadedData != null)
        {
            ReconstructPlayerData(loadedData);

            Debug.Log($"Player data loaded from: {PlayerDataPath}");
        }
        else
        {
            Debug.Log("Player's saved data is currently unavailable.");
        }
    }

    /// <summary>
    /// Helper method to deserialise information and load 
    /// it straight onto the relevant player stats classes.
    /// </summary>
    /// <param name="loadedData"></param>
    private void ReconstructPlayerData(PlayerSaveData loadedData)
    {
        LoadStatsData(loadedData);
        LoadPositionData(loadedData);
        LoadHealthData(loadedData);
        LoadStaminaData(loadedData);
        LoadLevelData(loadedData);
        LoadMoneyData(loadedData);
        RefreshUI();
    }

    private void LoadStatsData(PlayerSaveData loadedData)
    {
        Player.Speed = loadedData.Speed;
        Player.Damage = loadedData.Damage;
    }

    private void LoadPositionData(PlayerSaveData loadedData)
    {
        Player.GetComponent<Transform>().position = new Vector3(loadedData.PosX, loadedData.PosY, loadedData.PosZ);
    }

    private void LoadHealthData(PlayerSaveData loadedData)
    {
        Player.MaxHP = loadedData.MaxHP;
        Player.HealthPoints = loadedData.HealthPoints;
    }

    private void LoadStaminaData(PlayerSaveData loadedData)
    {
        Player.MaxStamina = loadedData.MaxStamina;
        Player.StaminaPoints = loadedData.StaminaPoints;
    }

    private void LoadLevelData(PlayerSaveData loadedData)
    {
        Player.Level.experience = loadedData.ExperiencePoints;
        Player.Level.experienceToNextLevel = loadedData.ExperienceToNextLevel;
        Player.Level.level = loadedData.Level;
    }

    private void LoadMoneyData(PlayerSaveData loadedData)
    {
        Player.Money.CurrentBalance = loadedData.CurrencyBalance;
    }

    private void RefreshUI()
    {
        Player.Level.UpdateUI();
        Player.Money.UpdateUI();
    }
}