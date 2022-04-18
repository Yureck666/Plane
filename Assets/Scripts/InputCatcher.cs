using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputCatcher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlaneMoveController planeMoveController;
        
    private float _beginDrugX;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginDrugX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        planeMoveController.Rotate(_beginDrugX - eventData.position.x);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        planeMoveController.StopRotate();
    }
}
