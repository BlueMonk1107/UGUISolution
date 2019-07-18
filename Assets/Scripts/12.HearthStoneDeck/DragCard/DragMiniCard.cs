using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMiniCard : DragCardBase
{
    private MiniCard _miniCard;
    private Image _image;
    private DraggingState _draggingState;

    public override void Init(DraggingRoot draggingRoot, Action endDrag, CardPoolMgr cardPoolMgr)
    {
        base.Init(draggingRoot, endDrag, cardPoolMgr);
        _miniCard = GetComponent<MiniCard>();
        _image = GetComponent<Image>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        _draggingState = DraggingState.Motionless;
        GetComponent<MiniCard>().SetGraphicState(true);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        JudgeDirection(eventData);
    }

    public override void EndDrag(bool isEnd, Transform lastCard)
    {
        if (lastCard == null)
            return;

        var cardTemp = _draggingRoot.CurrentDraggingCard;
        if (cardTemp.SizeType == SizeType.NormalCard)
            _cardPoolMgr.Despawn(cardTemp.CardType.ToString(), _draggingRoot.CurDraggingCardTrans);

        if (isEnd)
        {
            _cardPoolMgr.Despawn(SizeType.MiniCard.ToString(), transform);
            _endDrag();
        }
        else
        {
            _miniCard.SetGraphicState(false);
            _image.raycastTarget = false;
        }

        if (isEnd)
        {
            _draggingRoot.End();
        }
    }

    void JudgeDirection(PointerEventData eventData)
    {
        if (_draggingState == DraggingState.Motionless)
        {
            //判断是左右还是上下，通过设置对立的参数为0进行判断，
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                _draggingState = DraggingState.HorizontalDrag;
                _draggingRoot.SetDraggingCard(transform);
            }
            else
            {
                _draggingState = DraggingState.VerticalDrag;
            }

        }

        if (_draggingState == DraggingState.HorizontalDrag)
        {
            SetTheDraggingState(eventData);
        }
        else if(_draggingState == DraggingState.VerticalDrag)
        {
            RectTransform temp = transform.parent.GetComponent<RectTransform>();
            temp.DOAnchorPosY(temp.anchoredPosition.y + eventData.delta.y * 2f, 0.2f);
        }
        
    }

    private void SetTheDraggingState(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
}

public enum DraggingState
{
    /// <summary>
    /// 静止的
    /// </summary>
    Motionless,
    /// <summary>
    /// 横向拖动
    /// </summary>
    HorizontalDrag,
    /// <summary>
    /// 纵向拖动
    /// </summary>
    VerticalDrag
}
