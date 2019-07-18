using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Action _onBeginDrag;
    private Action _onDrag;
    private Action _onEndDrag;

    public void Init(Action onBeginDrag, Action onDrag, Action onEndDrag)
    {
        _onBeginDrag = onBeginDrag;
        _onDrag = onDrag;
        _onEndDrag = onEndDrag;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_onBeginDrag != null)
            _onBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_onDrag != null)
            _onDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_onEndDrag != null)
            _onEndDrag();
    }
}
