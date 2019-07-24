using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCard : MonoBehaviour,ICard
{
    public CardModel Model { get; private set; }
    public CardType Type { get; private set; }
    public SizeType Size { get { return SizeType.MiniCard;} }
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

    public void Init(CardModel model)
    {
        Model = model;
        Type = (CardType)model.Type;

        InitIcon(model.SpriteName);
        InitCost(model.Cost);
        SetRaycastState(true);
        SetGraphicState(true);
    }

    public void SetRaycastState(bool isReceive)
    {
        RaycasetImage.raycastTarget = isReceive;
    }

    private void InitIcon(string spriteName)
    {
        GetIconImage().sprite = Resources.Load<Sprite>("Card/" + spriteName);
    }

    private Image GetIconImage()
    {
        return transform.Find("BG/IconMask/Icon").GetComponent<Image>();
    }

    private void InitCost(int cost)
    {
        transform.Find("BG/Cost").GetComponent<Image>().sprite = GetCostSprite(cost);
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
