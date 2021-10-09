using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellAreaController : MonoBehaviour, IDropHandler
{
    public event Action OnDropEvent;
    public Money MoneyModel;

    // Start is called before the first frame update
    void Start()
    {
        if(MoneyModel == null)
        {
            MoneyModel = FindObjectOfType<Money>();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(OnDropEvent != null)
        {
            OnDropEvent();
        }
        //OnDropEvent?.Invoke(); // Handle item drop in the UIManager.
    }
}
