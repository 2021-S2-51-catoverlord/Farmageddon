using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInventory : MonoBehaviour
{
    [SerializeField] Text stackText;
    [SerializeField] GameObject currentObject;
    [SerializeField] Item item;

    void Start()
    {
        currentObject = gameObject;
        currentObject.gameObject.name = item.localName;
        Transform itemSpawn = GameObject.Find("Canvas").transform.Find("Inventory").transform.Find("ItemsSpawn").transform.Find(item.categoryName).transform;
        if (currentObject.name == item.localName)
        {
            Image getImage = currentObject.transform.Find("object").transform.GetComponent<Image>();
            getImage.sprite = item.icon;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        stackShowItems();
    }

    public void stackShowItems()
    {
        Transform itemsSpawnCounts = GameObject.Find("Canvas").transform.Find("Inventory").transform.Find("ItemsSpawn").transform.Find(item.categoryName).transform;
        Text stackText = itemsSpawnCounts.Find(item.categoryName + "_text").transform.GetComponent<Text>();
        stackText.transform.SetAsLastSibling(); //Set stack text on top of sprite
        for(int i = 0; i < itemsSpawnCounts.childCount; i++ )
        {
            int temp = i;
            stackText.text = temp.ToString("0");
            if(i == 0)
            {
                stackText.text = "";
            }
        }
      
    }

    public void UseItem()
    {
        
        Debug.Log("item Used" + item.localName);
        // this script is required for the removing system
        GameObject objectCounts = GameObject.Find("Canvas").transform.Find("Inventory").transform.Find("ItemsSpawn").transform.Find(item.categoryName).gameObject;
        if (objectCounts.transform.childCount == 2) // 1 is for the stack Counter
        {
            Destroy(objectCounts.gameObject);
        }

        if(item.itemType == "Equipment")
        {
            //Need to add functionally to equip equipment
        }
    }

    public void RemoveItem()
    {
        //Destroyed
        GameObject objectCounts = GameObject.Find("Canvas").transform.Find("Inventory").transform.Find("ItemsSpawn").transform.Find(item.categoryName).gameObject;
        Debug.Log(objectCounts.transform.childCount);
        if(objectCounts.transform.childCount == 2) // 1 is for the stack Counter
        {
            Destroy(objectCounts.gameObject);
        }

        // you can add more action to this such ass dropping instatinated item.
        //Instaitniate( new item); provided ex. source code.
        Destroy(currentObject);
    }
}
