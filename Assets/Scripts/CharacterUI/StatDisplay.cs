/*
 * This class contains the stats display for equipment UI,
 * which encapsulates the following methods:
 * Data:
 * 
 * Methods:
 * - 
 */
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Text nameTxt;
    public Text valueTxt;

    public void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameTxt = texts[0];
        valueTxt = texts[1];
    }
}
