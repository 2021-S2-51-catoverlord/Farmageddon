using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Equipment
{
    // Start is called before the first frame update
    public WeaponCategory weaponCat;
}

public enum WeaponCategory
{
    Sword
}
