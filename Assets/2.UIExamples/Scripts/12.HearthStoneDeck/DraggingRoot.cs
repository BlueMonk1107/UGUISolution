using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggingRoot : MonoBehaviour
{
    public ICard CurDraggingCard { get; set; }

    public DragCardBase CurDragComponent { get; set; }

    public Transform CurDraggingCardTrans { get; set; }

    public IAreaAction CurAreaAction { get; private set; }

    public void Init()
    {

    }

    public void SetDraggingCard(Transform card, DragCardBase dragCard, CardModel model)
    {
        SetNewCard(card, dragCard, model);
    }


    private void SetNewCard(Transform card, DragCardBase dragCard, CardModel model)
    {
        CurDraggingCardTrans = card;
        CurDraggingCard = card.GetComponent<ICard>();
        CurDragComponent = dragCard;
        CurAreaAction = CurDragComponent.GetComponent<IAreaAction>();

        CurDraggingCard.Init(model);

        SetCardRaycastState(false);
        InitCard(card);
    }

    private void SetCardRaycastState(bool isReceive)
    {
        if(CurDraggingCard == null)
            return;

        CurDraggingCard.SetRaycastState(isReceive);
    }

    private void InitCard(Transform card)
    {
        card.SetParent(transform);
        card.localPosition = Vector3.zero;
    }

    public void Clear()
    {
        CurDraggingCard = null;
        CurDragComponent = null;
        CurDraggingCardTrans = null;
        CurAreaAction = null;
    }
}
