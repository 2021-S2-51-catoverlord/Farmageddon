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
    [SerializeField] GameObject sellArea;
    [SerializeField] PlayerController player;

    public void Awake()
    {
        if(inventoryUI == null || equipUI == null || tooltip == null || sellArea == null || player == null)
        {
            inventoryUI = Resources.FindObjectsOfTypeAll<Inventory>()[0].gameObject;
            equipUI = Resources.FindObjectsOfTypeAll<EquipUI>()[0].gameObject;
            tooltip = Resources.FindObjectsOfTypeAll<ItemToolTip>()[0].gameObject;
            sellArea = Resources.FindObjectsOfTypeAll<SellArea>()[0].gameObject;
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }

    public void Start()
    {
        inventoryUI.SetActive(false);
        equipUI.SetActive(false);
        sellArea.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            sellArea.SetActive(inventoryUI.activeSelf);
            player.IsInventoryActive = inventoryUI.activeSelf;
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
