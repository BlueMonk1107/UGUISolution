using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggingRoot : MonoBehaviour
{
    public ICard CurDraggingCard { get; set; }

    public DragCardBase CurDragComponent { get; set; }

    public Transform CurDraggingCardTrans { get; set; }

    public void Init()
    {

    }

    public void SetDraggingCard(Transform card, DragCardBase dragCard)
    {
        if(CurDraggingCardTrans != null)
        {
            SetCardRaycastState(true);
        }

        CardModel model = new CardModel();
        if (CurDraggingCard != null)
        {
            model = CurDraggingCard.Model;
        }

        CurDraggingCardTrans = card;
        CurDraggingCard = card.GetComponent<ICard>();
        CurDragComponent = dragCard;

        CurDraggingCard.Init(model);

        SetCardRaycastState(false);
        InitCard(card);

    }

    private void SetCardRaycastState(bool isReceive)
    {
        CurDraggingCardTrans.GetComponent<Image>().raycastTarget = isReceive;
    }

    private void InitCard(Transform card)
    {
        card.SetParent(transform);
        card.localPosition = Vector3.zero;
    }
}
