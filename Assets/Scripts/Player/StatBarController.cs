using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Used for the stat bar (health/stamina).
*/
public class StatBarController : MonoBehaviour
{
    public Slider slider;

    //Set max value.
    public void SetMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
    
    //Set current value.
    public void SetCurrentValue(int currentValue)
    {
        slider.value = currentValue;
    }
}
