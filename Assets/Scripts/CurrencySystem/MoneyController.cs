using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    protected Money MoneyModel;

    // Start is called before the first frame update
    void Start()
    {
        if(MoneyModel == null)
        {
            // Get the Money script that is attached to one
            // of Gold Bar's components and save its reference.
            MoneyModel = GetComponentInChildren<Money>();
        }
    }

    // Update is called once per frame
    void Update()
    {   // Key to test AddMoney (increase balance) method.
        if (Input.GetKey(KeyCode.N))
        {
            MoneyModel.AddMoney(5);
        }

        // Key to test SubtractMoney (decrease balance) method.
        if(Input.GetKey(KeyCode.B))
        {
            MoneyModel.SubtractMoney(5);
        }
    }
}
