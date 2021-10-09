using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text DisplayUIText;
    protected int CurrentBalance;

    // use this for initialization
    void Start()
    {
        if(DisplayUIText == null)
        {
            // Reference the attached GameObject's Text component to this script.
            DisplayUIText = GetComponent<Text>();
        }

        CurrentBalance = 10;
        DisplayUIText.text = CurrentBalance.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //adding money into player's wallet
    public void AddMoney(int moneyToadd)
    {
        CurrentBalance += moneyToadd;
        DisplayUIText.text = CurrentBalance.ToString();
    }

    //subtract money from player's wallet
    public void SubtractMoney(int moneyToSubtract)
    {
        if(CurrentBalance - moneyToSubtract < 0)
        {
            Debug.Log(" insufficent money"); // once the money is insufficent the banner will pop out
        }
        else
        {
            CurrentBalance -= moneyToSubtract;
            DisplayUIText.text = CurrentBalance.ToString();
        }
    }
}