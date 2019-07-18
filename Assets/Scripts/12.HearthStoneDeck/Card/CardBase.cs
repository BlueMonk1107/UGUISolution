using UnityEngine;
using UnityEngine.UI;

public abstract class CardBase : MonoBehaviour, ICard
{
    public CardType CardType { get; private set; }
    public CardModel Model { get; private set; }
    public SizeType SizeType { get; private set; }
    public RectTransform Rect { get; private set; }

    public virtual void Init(SizeType size, CardModel model)
    {
        SizeType = size;
        Model = model;
        Rect = GetComponent<RectTransform>();
        CardType = (CardType)model.Type;
        InitIcon(model.SpriteName);
        InitRarity((RarityType)model.RarityType);
        InitName(model.Name);
        InitCost(model.Cost);
    }

    private void InitIcon(string spriteName)
    {
        GetIconImage().sprite = Resources.Load<Sprite>("Card/" + spriteName);
    }

    private void InitRarity(RarityType rarity)
    {
        transform.Find("Rarity").GetComponent<Image>().sprite = GetRatitySprite(rarity);
    }

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

    protected abstract Image GetIconImage();

    protected abstract Sprite GetRatitySprite(RarityType rarity);
   
}