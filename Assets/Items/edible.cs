using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : Mat
{
    private int healthGain;
    private int StaminaGain;
    public Edible(string itemName, int ID, string description, int value, int healthGain, int staminaGain) : base(itemName, ID, description, value)
    {
        this.healthGain = healthGain;
        this.StaminaGain = staminaGain;
    }

    /*
     * todo: add method, requires addition code elsewere
     */
    public void EatFood()
    {

    }
}