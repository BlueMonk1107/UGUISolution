using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardBase : MonoBehaviour,ICard
{
    public CardModel Model { get; private set; }
    public CardType Type { get; private set; }
    public SizeType Size {
        get { return SizeType.NormalCard;}
    }

    private Image _image;

    public Image RaycasetImage
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();

            return _image;
        }
    }

    public virtual void Init(CardModel model)
    {
        Model = model;
        Type = (CardType) model.Type;

        InitIcon(model.SpriteName);
        InitRarity((RarityType) model.RarityType);
        InitName(model.Name);
        InitCost(model.Cost);
        SetRaycastState(true);
    }

    public void SetRaycastState(bool isReceive)
    {
        RaycasetImage.raycastTarget = isReceive;
    }

    private void InitIcon(string spriteName)
    {
        GetIconImage().sprite = Resources.Load<Sprite>("Card/" + spriteName);
    }

    protected abstract Image GetIconImage();

    private void InitRarity(RarityType rarity)
    {
        transform.Find("Rarity").GetComponent<Image>().sprite = GetRaritySprite(rarity);
    }

    protected abstract Sprite GetRaritySprite(RarityType rarity);


    private void InitName(string cardName)
    {
        transform.Find("Name/Text").GetComponent<Text>().text = cardName;
    }

    private void InitCost(int cost)
    {
        transform.Find("Cost").GetComponent<Image>().sprite = GetCostSprite(cost);
    }

    private Sprite GetCostSprite(int cost)
    {
        var sprites = Resources.LoadAll<Sprite>("Card/TextInlineImages");
        return sprites[cost];
    }
}
