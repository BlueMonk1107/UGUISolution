using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DragCardBase : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    protected CardPoolMgr _poolMgr;
    public ICard Card { get; private set; }
    protected DraggingRoot _draggingRoot;
    protected Action _onEnd;

    public virtual void Init(CardPoolMgr poolMgr, DraggingRoot draggingRoot)
    {
        _poolMgr = poolMgr;
        _draggingRoot = draggingRoot;
        Card = GetComponent<ICard>();
        _onEnd = null;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _draggingRoot.transform.position = Input.mousePosition;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        _draggingRoot.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (_onEnd != null)
            _onEnd();
    }

    public void AddEndListener(Action end)
    {
        _onEnd = end;
    }
}
