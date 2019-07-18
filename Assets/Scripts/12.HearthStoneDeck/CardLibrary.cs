using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibrary : MonoBehaviour
{
    private CardPage _cardPage;
    private DeckList _deckList;
    private CardPoolMgr _cardPoolMgr;
    private DraggingRoot _draggingRoot;
    private int _cardNumMax = 8;

    // Start is called before the first frame update
    void Start()
    {
        _cardPoolMgr = new CardPoolMgr(transform);

        _cardPage = transform.Find("CardPage").gameObject.AddComponent<CardPage>();
        _deckList = transform.Find("DeckArea/DeckList").gameObject.AddComponent<DeckList>();
        _draggingRoot = transform.Find("DraggingRoot").gameObject.AddComponent<DraggingRoot>();
        DeckListArea area = transform.Find("DeckArea").gameObject.AddComponent<DeckListArea>();

        _draggingRoot.Init();
        _deckList.Init(_cardPoolMgr, _draggingRoot, area);
        _cardPage.Init(_cardNumMax, _cardPoolMgr, _draggingRoot);

        _cardPage.AddEndDragListener(_deckList.EndDrag);
    }
}
