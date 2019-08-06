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
        _draggingRoot.CurAreaAction.EnterArea();
    }

    public void ExitArea()
    {
        Debug.Log("ExitArea");
        _inArea = false;
        _draggingRoot.CurAreaAction.ExitArea();
    }

    public void EndDrag()
    {
        if (_inArea)
        {
            var model = _draggingRoot.CurDraggingCard.Model;
            var card = _poolMgr.Spwan(SizeType.MiniCard.ToString(), _content);
            card.GetComponent<ICard>().Init(model);
            card.GetComponent<IAreaAction>().Init(_poolMgr,_draggingRoot);
            DragCardBase dragCard = card.GetComponent<DragCardBase>();
            dragCard.Init(_poolMgr, _draggingRoot);
            dragCard.AddEndListener(EndDrag);
            _poolMgr.Despwan(SizeType.MiniCard.ToString(), _draggingRoot.CurDraggingCardTrans);
            _draggingRoot.Clear();
        }
        _inArea = false;
    }
}
