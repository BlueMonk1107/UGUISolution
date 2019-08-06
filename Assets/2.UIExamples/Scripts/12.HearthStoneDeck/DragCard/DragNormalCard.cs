using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNormalCard : DragCardBase
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        var cardTrans = _poolMgr.Spwan(Card.Type.ToString(), _draggingRoot.transform);
        _draggingRoot.SetDraggingCard(cardTrans, this, Card.Model);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        _poolMgr.Despwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.CurDraggingCardTrans);

        base.OnEndDrag(eventData);
    }
}
