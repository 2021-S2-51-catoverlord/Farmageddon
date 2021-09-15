using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {

        if (eventData.dragging)
        {
            Debug.Log("Dragging");
            transform.position = eventData.position;
        }
        else if (!eventData.dragging)
        {
            Debug.Log("Failed");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        Debug.Log("OnEndDrag");
    }
}