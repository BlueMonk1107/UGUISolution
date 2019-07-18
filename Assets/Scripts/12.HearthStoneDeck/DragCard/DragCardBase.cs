using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DragCardBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ICard Card { get; private set; }
    protected DraggingRoot _draggingRoot;
    protected CardPoolMgr _cardPoolMgr;
    protected Action _endDrag;

    public virtual void Init(DraggingRoot draggingRoot, Action endDrag, CardPoolMgr cardPoolMgr)
    {
        _draggingRoot = draggingRoot;
        Card = GetComponent<ICard>();
        _cardPoolMgr = cardPoolMgr;
        _endDrag = endDrag;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _draggingRoot.transform.position = Input.mousePosition;
        _draggingRoot.CurrentDragComponent = this;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        _draggingRoot.Rect.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag(true, _draggingRoot.LastCard);
    }

    public virtual void EndDrag(bool isEnd,Transform lastCard)
    {
    }
}
