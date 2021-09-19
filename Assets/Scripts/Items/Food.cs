using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Edible", menuName = "Inventory/Edible")]
public class Food : Item
{
    private int healthGain;
    private int StaminaGain;

    public void EatFood()
    {

    }

    public override Item GetItemCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }
}
