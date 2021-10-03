/*
 * This class contains the key inputs for the inventory,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - Start method
 * - Update method for key inputs
 */

using UnityEngine;

public class KeyInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject equipUI;
    [SerializeField] GameObject craftinUI;

    private void Start()
    {
        inventoryUI.SetActive(false);
        equipUI.SetActive(false);
        craftinUI = GameObject.FindWithTag("CraftingMenu");
        craftinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            equipUI.SetActive(!equipUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            craftinUI.SetActive(!craftinUI.activeSelf);
        }
    }
}
