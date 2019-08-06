using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCardAreaAction : MonoBehaviour, IAreaAction
{
    private CardPoolMgr _poolMgr;
    private DraggingRoot _draggingRoot;

    public void Init(CardPoolMgr poolMgr, DraggingRoot draggingRoot)
    {
        _poolMgr = poolMgr;
        _draggingRoot = draggingRoot;
    }

    public void EnterArea()
    {
        if (_draggingRoot.CurDraggingCard.Size == SizeType.NormalCard)
            _poolMgr.Despwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.CurDraggingCardTrans);

        SetCardGraphicState(true);
    }

    public void ExitArea()
    {
        SetCardGraphicState(false);

        var card = _draggingRoot.CurDraggingCard;
        var cardTemp = _poolMgr.Spwan(card.Type.ToString(), _draggingRoot.transform);
        _draggingRoot.SetDraggingCard(cardTemp, _draggingRoot.CurDragComponent, card.Model);

    }

    private void SetCardGraphicState(bool isShow)
    {
        _draggingRoot.CurDragComponent.GetComponent<MiniCard>().SetGraphicState(isShow);
    }
}
