using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Used for the health bar.
*/
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //Set max health
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    //Set current health
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
