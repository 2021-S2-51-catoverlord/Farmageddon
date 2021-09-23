using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    public Recipe item;
    [SerializeField] Image image;
    [SerializeField] Text amtTxt;
    public bool isValid;

    private Color normalColor = Color.white;
    private Color disableColor = Color.clear;

    protected virtual void OnValidate()
    {
        if (image == null) // if the image is empty then get image
        {
            image = GetComponent<Image>();
        }

        if (amtTxt == null) // 
        {
            amtTxt = GetComponentInChildren<Text>();
        }
    }



        // event methods
        public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left )
        {
            
        }
    }
}
