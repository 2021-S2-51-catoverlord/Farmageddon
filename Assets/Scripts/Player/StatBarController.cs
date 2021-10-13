/*
 * This class contains the stat UI for player,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - SetMaxValue: Sets the max value of the health/stamina bars.
 * - SetCurrentValue: Sets the current slider value for the health/stamina bars.
 */

using UnityEngine;
using UnityEngine.UI;

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
