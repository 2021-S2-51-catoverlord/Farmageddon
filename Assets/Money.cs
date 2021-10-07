using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Money : MonoBehaviour{

    public int money;
    public Text moneyText;
    // use this for initialization
    void Start()
    {
        money = 10;
        moneyText.text = money.ToString ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //adding money into player's wallet
    public void addMoney(int moneyToadd)
    {
        money += moneyToadd;
        moneyText.text = money.ToString();
    }

    //subtract money from player's wallet
    public void subtractMoney(int moneyToSubtract)
    {
    if(money - moneyToSubtract < 0)
        {
            Debug.Log(" insufficent money");// once the money is insufficent the banner will pop out
        }
        else
        {
            money -= moneyToSubtract;
            moneyText.text = money.ToString();
    }
}
    }