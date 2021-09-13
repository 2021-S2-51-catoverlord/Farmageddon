using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClose,ChestOpen;


    void Start()
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)

    {
        ChestClose.SetActive(false);
        ChestOpen.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
    }









}
