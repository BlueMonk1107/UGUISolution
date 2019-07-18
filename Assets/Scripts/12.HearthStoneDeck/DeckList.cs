using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckList : MonoBehaviour
{
    private CardPoolMgr _cardPoolMgr;
    private DraggingRoot _draggingRoot;
    private bool _inArea;

    // Start is called before the first frame update
    public void Init(CardPoolMgr cardPoolMgr, DraggingRoot draggingRoot, DeckListArea area)
    {
        _cardPoolMgr = cardPoolMgr;
        _draggingRoot = draggingRoot;
      
        area.Init(EnterArea, ExitArea);
    }

    private void EnterArea()
    {
        if(_draggingRoot.CurrentDraggingCard == null)
            return;
        
        _inArea = true;

        var size = _draggingRoot.CurrentDraggingCard.SizeType;
        if (size == SizeType.NormalCard && _draggingRoot.CurrentDragComponent.Card.SizeType == SizeType.NormalCard)
        {
            var card = _cardPoolMgr.Spawn(SizeType.MiniCard.ToString(), _draggingRoot.transform);
            card.GetComponent<DragMiniCard>().Init(_draggingRoot, EndDrag, _cardPoolMgr);

            var model = _draggingRoot.SetDraggingCard(card);
            MiniCard miniCard = card.GetComponent<MiniCard>();
            miniCard.Init(SizeType.MiniCard, model);
            miniCard.SetGraphicState(true);
        }
        else
        {
            if(_draggingRoot.CurrentDraggingCard.SizeType == SizeType.NormalCard)
                _cardPoolMgr.Despawn(_draggingRoot.CurrentDraggingCard.CardType.ToString(), ((MonoBehaviour)_draggingRoot.CurrentDraggingCard).transform);

            _draggingRoot.CurrentDragComponent.GetComponent<MiniCard>().SetGraphicState(true);
        }
    }

    private void ExitArea()
    {
        if (_draggingRoot.CurrentDraggingCard == null)
            return;

        _inArea = false;

        if(_draggingRoot.CurrentDragComponent.Card.SizeType == SizeType.MiniCard)
            _draggingRoot.CurrentDragComponent.GetComponent<MiniCard>().SetGraphicState(false);

        var model = _draggingRoot.CurrentDraggingCard.Model;
        var card = _cardPoolMgr.Spawn(((CardType)model.Type).ToString(), _draggingRoot.transform);
        _draggingRoot.SetDraggingCard(card);
    }

    public void EndDrag()
    {
        if (_inArea)
        {
            var model = _draggingRoot.CurrentDraggingCard.Model;
            var card = _cardPoolMgr.Spawn(SizeType.MiniCard.ToString(), transform.Find("Viewport/Content"));
            card.GetComponent<ICard>().Init(SizeType.MiniCard,model);
            card.GetComponent<MiniCard>().SetGraphicState(true);
            card.GetComponent<Image>().raycastTarget = true;
            _draggingRoot.End();
        }
        _inArea = false;
    }
}
