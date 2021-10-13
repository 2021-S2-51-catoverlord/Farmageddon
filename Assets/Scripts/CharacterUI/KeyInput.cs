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
    [SerializeField] GameObject tooltip;

    private void Start()
    {
        inventoryUI.SetActive(false);
        equipUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            //tooltip.SetActive(!tooltip.activeSelf);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            equipUI.SetActive(!equipUI.activeSelf);
            //tooltip.SetActive(!tooltip.activeSelf);
        }
    }
}
