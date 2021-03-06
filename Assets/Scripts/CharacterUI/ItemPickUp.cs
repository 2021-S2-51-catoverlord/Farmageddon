/*
 * This class contains the item tooltip for the inventory,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - Start method.
 * - Collision event (OnTriggerEnter2D)
 * - Pick up object and place in inventory (PickUp)
 */

using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;


    private void Start()
    {
        if (inventory == null) // If inventory has not been assigned yet...
        {
            inventory = Resources.FindObjectsOfTypeAll<Inventory>()[0];
        }
    }

    /// <summary>
    /// Method to handle collision event.
    /// </summary>
    /// <param name="target"></param>
    public void OnTriggerEnter2D(Collider2D target)
    {
        // If the player collided with the object...
        if (target.gameObject.tag == "Player")
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
        bool itemPickedUp = inventory.AddItem(item.GetItemCopy());

        // If the item has been successfully picked up...
        if (itemPickedUp)
        {
            // Make the game object disappear from the scene.
            Destroy(gameObject);
        }
    }
}
