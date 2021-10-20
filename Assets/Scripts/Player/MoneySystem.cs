using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public int CurrentBalance;
    
    public Text GoldBalText;
    //public PlayerController Player;

    // use this for initialization
    public void Awake()
    {
        if(GoldBalText == null)
        {
            GoldBalText = gameObject.GetComponentInChildren<Text>(); // Reference the attached GameObject child's Text component to this script.
        }

        //if(Player == null)
        //{
        //    Player = GameObject.Find("Player").GetComponent<PlayerController>();
        //}
    }

    public void Start()
    {
        CurrentBalance = 0;
        UpdateUI();
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.N))
        {
            AddMoney(5);
        }

        if(Input.GetKey(KeyCode.B))
        {
            SubtractMoney(5);
        }
    }

    //adding money into player's wallet
    public void AddMoney(int amount)
    {
        CurrentBalance += amount;
        UpdateUI();
    }

    //subtract money from player's wallet
    public void SubtractMoney(int amount)
    {
        if(CurrentBalance - amount < 0)
        {
            Debug.Log("Insufficent money!");
        }
        else
        {
            CurrentBalance -= amount;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        GoldBalText.text = $"$ {CurrentBalance}";
    }
}