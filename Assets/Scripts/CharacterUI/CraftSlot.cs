using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Recipe recipe;
    [SerializeField]
    private CraftingManager cManager;
    [SerializeField] Image image;
    [SerializeField] Text amtTxt;
    public bool isValid;
    private Inventory playerInv;
    private ItemToolTip toolTip;

    private Color normalColor = Color.white;
    private Color disableColor = Color.clear;

    public Recipe Recipe { get => recipe; set => recipe = value; }

    private void Start()
    {
        if (recipe != null)
        {
            image.sprite = recipe.item.icon;
            image.color = normalColor;
        }
        else
        {
            image.color = disableColor;
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null) // if the image is empty then get image
        {
            image = GetComponent<Image>();
        }
        if (playerInv == null) // if player inv is empty find the player inv
        {
            playerInv = Resources.FindObjectsOfTypeAll<Inventory>()[0];

        }
        if (amtTxt == null) // 
        {
            amtTxt = GetComponentInChildren<Text>();
        }
        if (toolTip == null)
        {
            toolTip = Resources.FindObjectsOfTypeAll<ItemToolTip>()[0];
        }
    }

    // event methods
    public void OnPointerClick(PointerEventData eventData)
    {
        cManager.UpdateInv();
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left && isValid)
        {
            for (int i = 0; i < recipe.RequiredItem.Length; i++)
            {
                playerInv.RemoveMultipleItems(recipe.RequiredItem[i], recipe.QuantityRequired[i]);
            }
            playerInv.AddItem(recipe.item);
        }
        else
        {
            Debug.Log(eventData.ToString());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (recipe != null)
        {
            toolTip.ShowRecipeTip(recipe);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (recipe !=  null)
        {
            toolTip.HideTooltip();
        }
    }
}
