using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList : MonoBehaviour
{
    private CardPoolMgr _poolMgr;
    private DraggingRoot _draggingRoot;
    private bool _inArea;
    private Transform _content;

    public void Init(CardPoolMgr poolMgr,DraggingRoot draggingRoot)
    {
        _poolMgr = poolMgr;
        _draggingRoot = draggingRoot;
        _inArea = false;
        _content = transform.Find("Viewport/Content");
    }

    public void EnterArea()
    {
        Debug.Log("EnterArea");
        _inArea = true;
        var size = _draggingRoot.CurDraggingCard.Size;
        if (size == SizeType.NormalCard && _draggingRoot.CurDragComponent.Card.Size == SizeType.NormalCard)
        {
            var miniCard = _poolMgr.Spwan(SizeType.MiniCard.ToString(), _draggingRoot.transform);
            _poolMgr.Despwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.CurDraggingCardTrans);
            _draggingRoot.SetDraggingCard(miniCard, _draggingRoot.CurDragComponent);
        }
        else
        {
            if(_draggingRoot.CurDraggingCard.Size == SizeType.NormalCard)
                _poolMgr.Despwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.CurDraggingCardTrans);

            _draggingRoot.CurDragComponent.GetComponent<MiniCard>().SetGraphicState(true);
        }
       
    }

    public void ExitArea()
    {
        Debug.Log("ExitArea");
        _inArea = false;
        var card = _poolMgr.Spwan(_draggingRoot.CurDraggingCard.Type.ToString(), _draggingRoot.transform);
        _poolMgr.Despwan(SizeType.MiniCard.ToString(), _draggingRoot.CurDraggingCardTrans);
        _draggingRoot.SetDraggingCard(card, _draggingRoot.CurDragComponent);
    }

    public void EndDrag()
    {
        if (_inArea)
        {
            var model = _draggingRoot.CurDraggingCard.Model;
            var card = _poolMgr.Spwan(SizeType.MiniCard.ToString(), _content);
            card.GetComponent<ICard>().Init(model);
            card.GetComponent<DragCardBase>().Init(_poolMgr, _draggingRoot);
            _poolMgr.Despwan(SizeType.MiniCard.ToString(), _draggingRoot.CurDraggingCardTrans);
        }
    }
}
