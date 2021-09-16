using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    /// <summary>
    /// Method to handle collision event.
    /// </summary>
    /// <param name="target"></param>
    public void OnTriggerEnter2D(Collider2D target)
    {
        // If the player collided with the object...
        if(target.gameObject.tag == "Player")
        {
            PickUp();
        }
    }

    /// <summary>
    /// Method to pick up the object and place it in the inventory (if possible).
    /// </summary>
    public void PickUp()
    {
        // Adds the picked up item to the inventory.
        bool itemPickedUp = Inventory.instance.AddI(item);

        // If the item has been successfully picked up...
        if (itemPickedUp)
        {
            // Make the game object disappear from the scene.
            CreateCategoryObject();
            CreateUIObject();
            Destroy(gameObject);
        }
    }

    public void CreateCategoryObject()
    {
        Transform ItemCanvas = GameObject.Find("Canvas").transform.Find("Inventory").transform.Find("ItemsSpawn").transform;
        if(!ItemCanvas.Find(item.categoryName))
        {
            GameObject categoryName = Instantiate(item.categoryObject, ItemCanvas) as GameObject;
            categoryName.name = item.categoryName;
            GameObject stackTextView = Instantiate(item.stackViewDisplay, categoryName.transform) as GameObject;
            stackTextView.name = item.categoryName + "_text";
        }
        else if(ItemCanvas.Find(item.categoryName))
        {
            Debug.Log("Already created");
        }
        
    }

    public void CreateUIObject()
    {
        Transform createPosition = GameObject.Find("Canvas").transform.Find("Inventory").transform.Find("ItemsSpawn").transform.Find(item.categoryName).transform;
        GameObject objectName = Instantiate(item.itemObject, createPosition);
        objectName.name = item.name;
    }
}
