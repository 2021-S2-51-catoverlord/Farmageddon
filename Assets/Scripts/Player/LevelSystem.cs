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

    private void Start()
    {
        level = 1;
        experienceToNextLevel = 32;

        levelUpSlider.value = experience;
        levelUpSlider.maxValue = experienceToNextLevel;

        currentLevel.text = "1";
        experienceTxt.text = experience.ToString() + "/" + experienceToNextLevel.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        //GetComponent<PlayerController>().IncreaseHealth(level);
        //GetComponent<PlayerController>().IncreaseStamina(level);
    }

    public void GainEXP(int exp)
    {
        experience += exp;
    }
}
