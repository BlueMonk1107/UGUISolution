using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMiniCard : DragCardBase
{
    private MiniCard _miniCard;
    private Direction _direction;
    private RectTransform _content;

    public override void Init(CardPoolMgr poolMgr, DraggingRoot draggingRoot)
    {
        base.Init(poolMgr, draggingRoot);
        _miniCard = GetComponent<MiniCard>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        _miniCard.SetGraphicState(true);
        _direction = Direction.NONE;
        _content = transform.parent.GetComponent<RectTransform>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        JudgeDirection(eventData);
        ExcuteDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_draggingRoot.CurDraggingCard == null)
            return;
        var card = _draggingRoot.CurDraggingCard;
        if(card.Size == SizeType.NormalCard)
            _poolMgr.Despwan(card.Type.ToString(),_draggingRoot.CurDraggingCardTrans);

        _poolMgr.Despwan(SizeType.MiniCard.ToString(), transform);

        base.OnEndDrag(eventData);
    }

    private void JudgeDirection(PointerEventData eventData)
    {
        if (_direction == Direction.NONE)
        {
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                _direction = Direction.HORIZONTAL_DRAG;
                _draggingRoot.SetDraggingCard(transform, this, Card.Model);
            }
            else
            {
                _direction = Direction.VERTICAL_DRAG;
            }
        }
    }

    private void ExcuteDrag(PointerEventData eventData)
    {
        if (_direction == Direction.HORIZONTAL_DRAG)
        {
            base.OnDrag(eventData);
        }
        else if(_direction == Direction.VERTICAL_DRAG)
        {
            _content.DOAnchorPosY(_content.anchoredPosition.y + eventData.delta.y*2, 0.2f);
        }
    }
}

public enum Direction
{
    NONE,
    HORIZONTAL_DRAG,
    VERTICAL_DRAG
}
