/*
 * This class contains the item slots for the inventory,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - OnValidate Method
 * - CanReceiveItem: inheritable method to check if item can be received
 * - CanAddStack: Add stacks until ItemSlot is full
 * - Event methods: OnPointerClick, OnBeginDrag, OnDrag
 *      OnEndDrag, OnDrop, OnPointerEnter, OnPointerExit
 * - Getter and Setter for Item and Amount.
 */

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected Item item;
    [SerializeField] Image image;
    [SerializeField] Text amtTxt;
    [SerializeField] ItemToolTip tooltip;
    private int amount;

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disableColor = Color.clear;

    protected virtual void OnValidate()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }

        if(tooltip == null)
        {
            tooltip = FindObjectOfType<ItemToolTip>();
        }

        if(amtTxt == null)
        {
            amtTxt = GetComponentInChildren<Text>();
        }
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public virtual bool CanAddStack(Item item, int amt = 1)
    {
        return Item != null && Item.ID == item.ID && amount + amt <= item.MaxStacks;
    }

    /// Event methods----------------------------------------------------------------------

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item is Tool || item is Weapon || item is Equipment)
        {
            tooltip.ShowTooltip((Equipment)Item);
        }
        if(Item is Food)
        {
            tooltip.ShowTooltip((Food)Item);
        }
        if(Item is Seed)
        {
            tooltip.ShowTooltip((Seed)Item);
        }
        if(Item is Mat)
        {
            tooltip.ShowTooltip((Mat)Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item is Tool || item is Weapon || item is Equipment)
        {
            tooltip.ShowTooltip((Equipment)Item);
        }
        if (Item is Food)
        {
            tooltip.ShowTooltip((Food)Item);
        }
        if (Item is Seed)
        {
            tooltip.ShowTooltip((Seed)Item);
        }
        if (Item is Mat)
        {
            tooltip.ShowTooltip((Mat)Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    /// Getter and Setters-----------------------------------------------------------------
    public Item Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            if(item == null && amount != 0)
            {
                amount = 0;
            }
            if(item == null)
            {
                image.color = disableColor;
            }
            else
            {
                image.sprite = item.icon;
                image.color = normalColor;
            }
        }
    }

    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;

            if(amount == 0 && item != null)
            {
                item = null;
            }
            if(amount < 0)
            {
                amount = 0;
            }
            if(amtTxt != null)
            {
                amtTxt.enabled = item != null && amount > 1;
                if(amtTxt.enabled)
                {
                    amtTxt.text = amount.ToString();
                }
            }
        }
    }

    public override string ToString()
    {
        return item.name; 
    }
}
