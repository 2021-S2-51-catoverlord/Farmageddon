using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour
{

    [SerializeField] GameObject SellBox;
    [SerializeField] Inventory inventory;

    private int amount;

    // Start is called before the first frame update
    void Start()
    {
        SellBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SellBox.SetActive(!SellBox.activeSelf);
        }
    }
    public void SellItem()
    {
       // if(Item)
    }


}
