using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MiniCard : MonoBehaviour, ICard
{
    public CardType CardType { get; private set; }
    public CardModel Model { get; private set; }
    public SizeType SizeType { get; private set; }
    public void Init(SizeType size, CardModel model)
    {
        SizeType = size;
        Model = model;
        CardType = (CardType)model.Type;
        InitIcon(model.SpriteName);
        InitCost(model.Cost);
    }

    private void InitIcon(string spriteName)
    {
        GetIconImage().sprite = Resources.Load<Sprite>("Card/" + spriteName);
    }

    private void InitCost(int cost)
    {
        transform.Find("BG/Cost").GetComponent<Image>().sprite = GetCostSprite(cost);
    }

    private Image GetIconImage()
    {
        return transform.Find("BG/IconMask/Icon").GetComponent<Image>();
    }

    private Sprite GetCostSprite(int cost)
    {
        var sprites = Resources.LoadAll<Sprite>("Card/TextInlineImages");
        return sprites[cost];
    }

    public void SetGraphicState(bool show)
    {
        transform.Find("BG").gameObject.SetActive(show);
    }
}
