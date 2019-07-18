using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggingRoot : MonoBehaviour
{
    public RectTransform Rect { get; private set; }
    public ICard CurrentDraggingCard { get; set; }
    public Transform CurDraggingCardTrans { get; set; }
    public DragCardBase CurrentDragComponent { get; set; }

    public Transform LastCard { get; set; }

    public void Init()
    {
        Rect = GetComponent<RectTransform>();
    }
    
    public void End()
    {
        SetImageRaycastState(true);
        CurrentDraggingCard = null;
        CurrentDragComponent = null;
        LastCard = null;

    }

    public CardModel SetDraggingCard(Transform card)
    {
        CardModel model = new CardModel();
        if (LastCard != null)
        {
            model = LastCard.GetComponent<ICard>().Model;
        }

        if (LastCard != card)
        {
            CurrentDragComponent.EndDrag(false, LastCard);
        }
        else
        {
            SetImageRaycastState(true);
        }

        LastCard = card;
        CurrentDraggingCard = card.GetComponent<ICard>();
        InitDraggingCard(card);
        SetImageRaycastState(false);

        return model;
    }

    private void InitDraggingCard(Transform card)
    {
        CurDraggingCardTrans = card;
        card.SetParent(transform);
        card.localPosition = Vector3.zero;
    }

    private void SetImageRaycastState(bool isReceive)
    {
        if (LastCard == null)
            return;

        LastCard.GetComponent<Image>().raycastTarget = isReceive;
    }
}
