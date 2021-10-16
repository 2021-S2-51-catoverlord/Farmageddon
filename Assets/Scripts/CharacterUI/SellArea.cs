using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SellArea : MonoBehaviour, IDropHandler
{
    public const string PresaleTxt = "Drop item here to sell $$$$$";
    public const string PostsaleTxt = "Item Sold";

    public Text SellAreaText;
    public event Action OnDropEvent;

    public void Awake()
    {
        if(SellAreaText == null)
        {
            SellAreaText = gameObject.GetComponentInChildren<Text>();
            SellAreaText.text = PresaleTxt;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(); // Handle item drop in the UIManager.
    }

    public IEnumerator ToggleText()
    {
        SellAreaText.text = PostsaleTxt;
        yield return new WaitForSeconds(1f);
        SellAreaText.text = PresaleTxt;
    }
}
