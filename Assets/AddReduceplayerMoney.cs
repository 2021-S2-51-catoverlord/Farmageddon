using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddReduceplayerMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gold;

    void Start()
    {
        Gold = GameObject.Find("Gold");
    }

    // Update is called once per frame
    void Update()
    {   //the quick function key
        if (Input.GetKey(KeyCode.B))
        {
            Gold.GetComponent<Money>().addMoney(5);
        }
        //the quick function key 2
        if (Input.GetKey(KeyCode.N))
        {
            Gold.GetComponent<Money>().subtractMoney(5);
        }
    }
}
