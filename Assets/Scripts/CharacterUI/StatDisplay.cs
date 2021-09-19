using System.Collections;
using System.Collections.Generic;
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
