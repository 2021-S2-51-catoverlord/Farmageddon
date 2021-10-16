using System;

[Serializable]
public class PlayerSaveData
{
    public string EntityName;
    public float Speed;
    public int Damage;

    public float PosX, PosY, PosZ;

    public int MaxHP, HealthPoints;
    public int MaxStamina, StaminaPoints;

    public int ExperiencePoints, ExperienceToNextLevel, Level;

    public int CurrencyBalance;

    /// <summary>
    /// Default constructor for the use of player's data persistence.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="level"></param>
    public PlayerSaveData(PlayerController player)
    {
        EntityName = player.EntityName;
        Speed = player.Speed;
        Damage = player.Damage;

        PosX = player.transform.position.x;
        PosY = player.transform.position.y;
        PosZ = player.transform.position.z;

        MaxHP = player.MaxHP;
        HealthPoints = player.HealthPoints;
        MaxStamina = player.MaxStamina;
        StaminaPoints = player.StaminaPoints;

        ExperiencePoints = player.Level.experience;
        ExperienceToNextLevel = player.Level.experienceToNextLevel;
        Level = player.Level.level;

        CurrencyBalance = player.Money.CurrentBalance;
    }
}