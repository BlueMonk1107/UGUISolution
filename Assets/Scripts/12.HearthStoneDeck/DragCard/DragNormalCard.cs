using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNormalCard : DragCardBase
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        Transform draggingCard = GetCard( _draggingRoot.transform);
        _draggingRoot.SetDraggingCard(draggingCard);
    }

    private Transform GetCard(Transform parent)
    {
        CardBase card = _cardPoolMgr.Spawn(Card.CardType.ToString(), parent).GetComponent<CardBase>();
        card.Init(SizeType.NormalCard, Card.Model);
        return card.transform;
    }

    public override void EndDrag(bool isEnd, Transform lastCard)
    {
        if (lastCard == null)
            return;

        var card = _draggingRoot.CurrentDraggingCard;
        string key = card.SizeType == SizeType.NormalCard ? card.CardType.ToString() : SizeType.MiniCard.ToString();
        _cardPoolMgr.Despawn(key, _draggingRoot.CurDraggingCardTrans);

        if (_endDrag != null && isEnd)
            _endDrag();

        if (isEnd)
        {
            _draggingRoot.End();
        }
    }
}
