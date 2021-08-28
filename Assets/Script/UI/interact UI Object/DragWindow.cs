using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform panelRectTrans;
    public void OnDrag(PointerEventData eventData)
    {
        panelRectTrans.anchoredPosition += eventData.delta;
    }
}
