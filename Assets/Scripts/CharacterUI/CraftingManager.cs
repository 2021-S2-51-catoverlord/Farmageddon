using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInv;
    [SerializeField]
    private CraftSlot[] craftSlots;
    [SerializeField]
    private static int MAXLOG = 25;
    private int logSize = 0;
    private InvLog[] invLog;

    private void Start()
    {
        invLog = new InvLog[MAXLOG];
        playerInv = Resources.FindObjectsOfTypeAll<Inventory>()[0];
    }

    private void UpdateInvList()
    {
        logSize = 0;
        int itemSlot = 0;
        for(int i = 0; i < playerInv.itemSlots.Length; i++)
        {
            if(playerInv.itemSlots[i].Item != null)
            {
                invLog[itemSlot] = new InvLog();

                invLog[itemSlot].Quantity = playerInv.itemSlots[i].Amount;
                invLog[itemSlot].Item = playerInv.itemSlots[i].Item;
                logSize++;
                itemSlot++;
            }
        }
    }
    public void UpdateInv()
    {
        UpdateInvList();
        CheckRecipes();
    }

    private void CheckRecipes()
    {
        int passRate = 0;
        //for each of the craft slots
        //i = craft slot
        for(int i = 0; i < craftSlots.Length; i++)
        {
            if(craftSlots[i].Recipe != null)
            {
                //for each of the items the recipe requires - this loop has escapes
                //J = required item
                for(int j = 0; j < craftSlots[i].Recipe.RequiredItem.Length; j++)
                {
                    //for each item the player has - this loop has escapes
                    //x = invLog
                    for(int x = 0; x < logSize; x++)
                    {
                        // if item is the correct ID and we have enough of the item set isValid to true and end the search
                        if(craftSlots[i].Recipe.RequiredItem[j] == invLog[x].Item && craftSlots[i].Recipe.QuantityRequired[j] <= invLog[x].Quantity)
                        {
                            passRate++;
                        }
                        //if the item is the correct ID but we dont have enough of the item isValid becomes false and we stop checking the CraftSlot
                        else if(craftSlots[i].Recipe.RequiredItem[j] == invLog[x].Item && craftSlots[i].Recipe.QuantityRequired[j] > invLog[x].Quantity)
                        {
                            x = logSize;
                            j = craftSlots[i].Recipe.RequiredItem.Length;
                        }
                    }
                    if(passRate == craftSlots[i].Recipe.RequiredItem.Length)
                    {
                        craftSlots[i].isValid = true;
                    }
                    else
                    {
                        craftSlots[i].isValid = false;

                    }
                }
            }
        }
    }
}

// small instance class to store a parsable log of the inventory
public class InvLog
{
    private Item item;
    private int quantity;

    public int Quantity { get => quantity; set => quantity = value; }
    public Item Item { get => item; set => item = value; }

    public override string ToString()
    {
        return Item.name + Quantity;
    }
}
