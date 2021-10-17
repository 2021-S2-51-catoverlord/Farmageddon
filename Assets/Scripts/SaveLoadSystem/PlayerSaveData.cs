using System;

[Serializable]
public class PlayerSaveData
{
    public float Speed;
    public int Damage;

    public float PosX, PosY, PosZ;

    public int MaxHP, HealthPoints;
    public int MaxStamina, StaminaPoints;

    public int ExperiencePoints, ExperienceToNextLevel, Level;

    public int CurrencyBalance;

    /// <summary>
    /// Default constructor for the use of player data's persistence.
    /// </summary>
    /// <param name="player"></param>
    public PlayerSaveData(PlayerController player)
    {
        SetStatsData(player);
        SetPositionData(player);
        SetHealthData(player);
        SetStaminaData(player);
        SetLevelData(player);
        SetMoneyData(player);
    }

    private void SetStatsData(PlayerController player)
    {
        Speed = player.Speed;
        Damage = player.Damage;
    }

    private void SetPositionData(PlayerController player)
    {
        PosX = player.transform.position.x;
        PosY = player.transform.position.y;
        PosZ = player.transform.position.z;
    }

    private void SetHealthData(PlayerController player)
    {
        MaxHP = player.MaxHP;
        HealthPoints = player.HealthPoints;
    }

    private void SetStaminaData(PlayerController player)
    {
        MaxStamina = player.MaxStamina;
        StaminaPoints = player.StaminaPoints;
    }

    private void SetLevelData(PlayerController player)
    {
        ExperiencePoints = player.Level.experience;
        ExperienceToNextLevel = player.Level.experienceToNextLevel;
        Level = player.Level.level;
    }

    private void SetMoneyData(PlayerController player)
    {
        CurrencyBalance = player.Money.CurrentBalance;
    }
}