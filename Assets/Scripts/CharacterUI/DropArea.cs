/*
 * This class contains the droparea for items to be destroyed,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - OnDrop Event.
 */

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public event Action OnDropEvent;

    public void OnDrop(PointerEventData eventData)
    {
        if(OnDropEvent != null)
        {
            OnDropEvent();
        }
    }
}
