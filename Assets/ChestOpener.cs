using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClose,ChestOpen;
    public GameObject ChestInventory;


    void Start()
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
        //ChestInventory = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)

    {
        ChestClose.SetActive(false);
        ChestOpen.SetActive(true);
        ChestInventory.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
        ChestInventory.SetActive(false);
    }









}
