using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> 매개변수를 전달하는 이벤트 핸들러 </summary>
public class UI_PivotEventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData, object> OnClickHandler = null;
    public object Pivot { get; set; } = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData, Pivot);
    }
}