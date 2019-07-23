using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardLibrary : MonoBehaviour
{
    private int _cardNumMax = 8;

    // Start is called before the first frame update
    void Start()
    {
        CardPoolMgr poolMgr = new CardPoolMgr(transform);
        CardPage cardPage = transform.Find("CardPage").gameObject.AddComponent<CardPage>();
        DeckList deckList = transform.Find("DeckArea/DeckList").gameObject.AddComponent<DeckList>();
        DeckListArea area = transform.Find("DeckArea").gameObject.AddComponent<DeckListArea>();
        DraggingRoot draggingRoot = transform.Find("DraggingRoot").gameObject.AddComponent<DraggingRoot>();

        area.Init(deckList.EnterArea, deckList.ExitArea);
        deckList.Init(poolMgr, draggingRoot);
        draggingRoot.Init();
        cardPage.Init(_cardNumMax, poolMgr, draggingRoot);
        cardPage.AddEndListener(deckList.EndDrag);
    }
}
