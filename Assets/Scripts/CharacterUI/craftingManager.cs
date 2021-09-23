using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingManager : MonoBehaviour
{

    private Inventory playerInv;
    [SerializeField]
    private CraftSlot[] craftSlots;
    [SerializeField]
    private static int MAXLOG = 25;
    private invLog[] invLog;
    private void Start()
    {
        playerInv = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        updateInvList();
        invLog = new invLog[MAXLOG];
    }

    private void updateInvList()
    {
        for (int i = 0; i < playerInv.ItemSlots.Length; i++)
        {
            int itemSlot = 0;
            if (playerInv.ItemSlots[i].Item != null)
            {
                invLog[itemSlot].item = playerInv.ItemSlots[i].Item;
                invLog[itemSlot].quantity = playerInv.ItemSlots[i].Amount;
                itemSlot++;
            }
        }
    } 

    public void checkRecipies()
    {
        //for each of the craft slots
        for (int i = 0; i < craftSlots.Length; i++)
        {
            //for each of the items the recipe requires - this loop has escapes
            for (int j = 0; j < craftSlots[i].item.RequiredItem.Length; j++)
            {   
                //for each item the player has - this loop has escapes
                for (int x = 0; x < MAXLOG; x++)
                {
                    //if we are at the end of the inv log end log loop
                    if (invLog[x] == null) 
                    {
                        x = MAXLOG;
                    }
                    // if item is the correct ID and we have enough of the item set isValid to true and end the search
                    else if (craftSlots[i].item.RequiredItem[j] ==invLog[j].item && 
                        craftSlots[i].item.QuantityRequired[j] <= invLog[j].quantity)
                    {
                        craftSlots[i].isValid = true;
                        x = MAXLOG;
                    }
                    //if the item is the correct ID but we dont have enough of the item isValid becomes false and we stop checking the CraftSlot
                    else if(craftSlots[i].item.RequiredItem[j] == invLog[j].item &&
                        craftSlots[i].item.QuantityRequired[j] > invLog[j].quantity)
                    {
                        craftSlots[i].isValid = false;
                        x = MAXLOG;
                        j = craftSlots[i].item.RequiredItem.Length;
                    }

                }
            }
        }
    }

}
public class invLog
{
    public Item item;
    public int quantity;
}



