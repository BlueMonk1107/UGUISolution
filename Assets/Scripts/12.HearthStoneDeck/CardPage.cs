using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CardPage : MonoBehaviour
{
    public int ID { get; private set; }

    private List<CardModel> _models;
    private int _countMax;
    private CardPoolMgr _cardPoolMgr;
    private DraggingRoot _draggingRoot;
    private Action _endDrag;

    public void Init(int countMax, CardPoolMgr cardPoolMgr, DraggingRoot draggingRoot)
    {
        _countMax = countMax;
        _draggingRoot = draggingRoot;
        _cardPoolMgr = cardPoolMgr;
        _models = GetCardsData();
        UpdateCard();
    }

    public void UpdateCard()
    {
        for (int i = ID * _countMax; i < (ID + 1) * _countMax; i++)
        {
            if (i < _models.Count)
            {
                CardBase card = _cardPoolMgr.Spawn(((CardType)_models[i].Type).ToString(), transform).GetComponent<CardBase>();
                card.Init(SizeType.NormalCard, _models[i]);
                card.GetComponent<DragCardBase>().Init(_draggingRoot, () => _endDrag(), _cardPoolMgr);
            }
        }
    }

    public void AddEndDragListener(Action endDrag)
    {
        _endDrag = endDrag;
    }

    public List<CardModel> GetCardsData()
    {
        return Service.Instance.GetCardsData();
    }
}
