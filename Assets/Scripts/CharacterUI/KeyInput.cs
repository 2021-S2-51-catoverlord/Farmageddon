using UnityEngine;

public class KeyInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject equipUI;
    [SerializeField] GameObject sellArea;

    private void Start()
    {
        if(sellArea == null)
        {
            sellArea = transform.parent.Find("Sell Area").gameObject;
        }
    
        inventoryUI.SetActive(false);
        equipUI.SetActive(false);
        sellArea.SetActive(false);
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

        if(Input.GetKeyDown(KeyCode.P))
        {
            sellArea.SetActive(!sellArea.activeSelf);
        }
    }
}
