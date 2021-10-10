using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    protected Item item;
    [SerializeField] Image image;
    [SerializeField] Text amtTxt;
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
        if(image == null) // if the image is empty then get image
        {
            image = GetComponent<Image>();
        }

        if(amtTxt == null)
        {
            amtTxt = GetComponentInChildren<Text>();
        }

        Item = item;
        Amount = amount;
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public virtual bool CanAddStack(Item item, int amt = 1)
    {
        return Item != null && Item.Id == item.Id && amount + amt <= item.MaxStacks;
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
}
