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
            Destroy(gameObject);
        }
    }
}
