using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Amos : MonoBehaviour
{
    [SerializeField]
    private GameObject itemsParentPrefab;

    [SerializeField]
    private GameObject inventoryTitlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(itemsParentPrefab, transform);
        Instantiate(inventoryTitlePrefab, transform);
    }
}
