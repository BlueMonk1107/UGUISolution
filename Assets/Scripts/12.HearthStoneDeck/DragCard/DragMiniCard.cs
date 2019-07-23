using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMiniCard : DragCardBase
{
    private MiniCard _miniCard;

    public override void Init(CardPoolMgr poolMgr, DraggingRoot draggingRoot)
    {
        base.Init(poolMgr, draggingRoot);
        _miniCard = GetComponent<MiniCard>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        _miniCard.SetGraphicState(true);
        _draggingRoot.SetDraggingCard(transform, this);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        _poolMgr.Despwan(SizeType.MiniCard.ToString(), transform);
    }
}
