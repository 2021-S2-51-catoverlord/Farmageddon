/*
 * This class contains the level up system for the player,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - Start method.
 * - Update Method.
 * - IncreaseLevel: Increases the player's level, calculate the next experience amount,
 *      increase health and stamina.
 * - GainEXP: increase experience points 
 */

using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public int experience;
    public int experienceToNextLevel;

    public Slider levelUpSlider;
    public Text currentLevel;
    public Text experienceTxt;
    public PlayerController player;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        level = 1;
        experienceToNextLevel = 32;

        levelUpSlider.value = experience;
        levelUpSlider.maxValue = experienceToNextLevel;

        currentLevel.text = "1";
        experienceTxt.text = experience.ToString() + "/" + experienceToNextLevel.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GainEXP(10);
            levelUpSlider.value = experience;
            experienceTxt.text = experience.ToString() + "/" + experienceToNextLevel.ToString();
        }

        if(levelUpSlider.value >= levelUpSlider.maxValue)
        {
            IncreaseLevel();
            experienceTxt.text = experience.ToString() + "/" + experienceToNextLevel.ToString();
        }
    }

    private void IncreaseLevel()
    {
        level++;
        currentLevel.text = level.ToString();
        levelUpSlider.value = 0;

        experience -= experienceToNextLevel;

        experienceToNextLevel = (int)(experienceToNextLevel * 1.8);
        levelUpSlider.maxValue = experienceToNextLevel;

        player.IncreaseHealth(level);
        player.IncreaseStamina(level);
    }

    public void GainEXP(int exp)
    {
        experience += exp;
    }
}
