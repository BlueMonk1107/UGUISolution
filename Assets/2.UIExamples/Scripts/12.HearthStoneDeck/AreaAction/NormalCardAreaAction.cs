using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCardAreaAction : MonoBehaviour, IAreaAction
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
        var miniCard = _poolMgr.Spwan(SizeType.MiniCard.ToString(), _draggingRoot.transform);
        _poolMgr.Despwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.CurDraggingCardTrans);
        _draggingRoot.SetDraggingCard(miniCard, _draggingRoot.CurDragComponent, _draggingRoot.CurDraggingCard.Model);
    }

    public void ExitArea()
    {
        var card = _poolMgr.Spwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.transform);
        _poolMgr.Despwan(SizeType.MiniCard.ToString(), _draggingRoot.CurDraggingCardTrans);
        _draggingRoot.SetDraggingCard(card, _draggingRoot.CurDragComponent, _draggingRoot.CurDraggingCard.Model);
    }
}
