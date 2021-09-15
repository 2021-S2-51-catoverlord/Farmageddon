using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Equipment
{
    public WeaponCategory weaponCat;
    public int weaponAtk;
}
public enum WeaponCategory
{
    Sword
}