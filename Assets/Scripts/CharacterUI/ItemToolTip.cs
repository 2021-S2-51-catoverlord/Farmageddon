/*
 * This class contains the item tooltip for the inventory,
 * which encapsulates the following methods:
 * Data:
 * - A Text field for the item name (itemNameText)
 * - A Text field for the item info (itemInfoText)
 * 
 * Methods:
 * - An Awake method which sets the tooltip to false during gameplay.
 * - ShowTooltip: Shows the item information when the mouse is over the item in the inventory.
 * - HideTooltip: Hides the tooltip.
 * - GetItemInfo: Retrieves basic item information.
 * - GetEquipmentInfo: Retrieves all information about the equipment.
 * - GetFoodInfo: Retrieves all information about food.
 * - GetMaterialInfo: Retrieves all information about materials.
 */

using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemInfoText;

    private readonly StringBuilder sb = new StringBuilder();

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowTooltip(Item item)
    {
        itemNameText.text = item.itemName;
        if(item is Food)
        {
            itemInfoText.text = GetFoodInfo((Food)item);
        }
        if(item is Tool || item is Weapon || item is Equipment)
        {
            itemInfoText.text = GetEquipmentInfo((Equipment)item);
        }
        if(item is Seed)
        {
            itemInfoText.text = GetItemInfo((Seed)item);
        }
        if(item is Mat)
        {
            itemInfoText.text = GetMaterialInfo((Mat)item);
        }

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public string GetItemInfo(Item item)
    {
        sb.Length = 0;

        sb.Append(item.itemDescription);
        sb.AppendLine();
        sb.AppendLine();
        sb.Append("Price: ");
        sb.Append(item.price);
        sb.AppendLine();

        return sb.ToString();
    }

    public string GetEquipmentInfo(Equipment equip)
    {
        GetItemInfo(equip);

        if(equip.attack > 0)
        {
            sb.Append("Atk: ");
            sb.Append(equip.attack);
            sb.AppendLine();
        }

        if(equip.defence > 0)
        {
            sb.Append("Def: ");
            sb.Append(equip.defence);
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string GetFoodInfo(Food food)
    {
        sb.Length = 0;

        GetItemInfo(food);

        if(food.healHeath > 0)
        {
            sb.Append("Heals ");
            sb.Append(food.healHeath);
            sb.Append("HP");
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string GetMaterialInfo(Mat material)
    {
        sb.Length = 0;

        GetItemInfo(material);
        sb.Append(material.materialCat);

        return sb.ToString();
    }
}