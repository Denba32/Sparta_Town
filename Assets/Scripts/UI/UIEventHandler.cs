using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnPointerDownHandler = null;
    public Action<PointerEventData> OnPointerUpHandler = null;

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownHandler != null)
            OnPointerDownHandler.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpHandler != null)
            OnPointerUpHandler.Invoke(eventData);
    }
}
