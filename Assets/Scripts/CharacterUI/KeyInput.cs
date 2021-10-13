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

    private void Awake()
    {
        if(inventoryUI == null || equipUI == null || tooltip ==null)
        {
            inventoryUI = GameObject.Find("Inventory");
            equipUI = GameObject.Find("EquipSlots");
            tooltip = GameObject.Find("Tooltip");
        }
    }
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
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            equipUI.SetActive(!equipUI.activeSelf);
        }

        if(!(equipUI.activeSelf || inventoryUI.activeSelf))
        {
            tooltip.SetActive(false);
        }
    }
}
