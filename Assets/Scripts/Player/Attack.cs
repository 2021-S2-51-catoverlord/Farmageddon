using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("trigger started"); 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test 1");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("test");
        }
    }

}
