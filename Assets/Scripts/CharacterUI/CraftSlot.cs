using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private Recipe recipe;
    [SerializeField]
    private craftingManager cManager;
    [SerializeField] Image image;
    [SerializeField] Text amtTxt;
    public bool isValid;
    private Inventory playerInv;

    private Color normalColor = Color.white;
    private Color disableColor = Color.clear;

    public Recipe Recipe { get => recipe; set => recipe = value; }

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
    }



        // event methods
        public void OnPointerClick(PointerEventData eventData)
    {
        cManager.UpdateInv();
        Debug.Log("craft");
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left && isValid)
        {
            playerInv.AddItem(recipe.item);
        }
        else
        {
            Debug.Log(eventData.ToString());
            Debug.Log("is valid: " + isValid);
        }
    }
}
