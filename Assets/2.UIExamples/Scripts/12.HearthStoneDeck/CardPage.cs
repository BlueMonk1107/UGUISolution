using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPage : MonoBehaviour
{
    private int _countMax;
    private CardPoolMgr _poolMg;
    private List<CardModel> _models;
    private DraggingRoot _draggingRoot;
    private Action _onEnd;

    public void Init(int countMax,CardPoolMgr poolMgr, DraggingRoot draggingRoot)
    {
        _countMax = countMax;
        _poolMg = poolMgr;
        _models = GetModels();
        _draggingRoot = draggingRoot;
        UpdateCard();
    }

    private void UpdateCard()
    {
        for (int i = 0; i < _countMax; i++)
        {
            if (i < _models.Count)
            {
                Transform cardTrans = _poolMg.Spwan(((CardType) _models[i].Type).ToString(), transform);
                ICard card = cardTrans.GetComponent<ICard>();
                card.Init(_models[i]);
                DragCardBase dragCard = cardTrans.GetComponent<DragCardBase>();
                dragCard.Init(_poolMg, _draggingRoot);
                dragCard.AddEndListener(()=> _onEnd());
                cardTrans.GetComponent<IAreaAction>().Init(_poolMg,_draggingRoot);
            }
        }
    }

    public List<CardModel> GetModels()
    {
        return Service.Instance.GetCardsData();
    }

    public void AddEndListener(Action end)
    {
        _onEnd = end;
    }
}
