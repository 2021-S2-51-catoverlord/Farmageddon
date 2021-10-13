/*
 * This class contains the stat UI for player,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - SetMaxValue: Sets the max value of the health/stamina bars.
 * - SetCurrentValue: Sets the current Slider value for the health/stamina bars.
 */

using UnityEngine;
using UnityEngine.UI;

public class StatBarController : MonoBehaviour
{
    public Slider Slider;

    public void Awake()
    {
        if(Slider == null)
        {
            Slider = GetComponent<Slider>();
        }
    }

    //Set max value.
    public void SetMaxValue(int maxValue)
    {
        Slider.maxValue = maxValue;
        Slider.value = maxValue;
    }

    //Set current value.
    public void SetCurrentValue(int currentValue)
    {
        Slider.value = currentValue;
    }
}
