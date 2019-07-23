using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionCard : CardBase
{
    public override void Init(CardModel model)
    {
        base.Init(model);
        InitAttack(model.Attack);
        InitLife(model.Life);
    }

    private void InitAttack(int num)
    {
        transform.Find("Attack/Text").GetComponent<Text>().text = num.ToString();
    }

    private void InitLife(int num)
    {
        transform.Find("Life/Text").GetComponent<Text>().text = num.ToString();
    }

    protected override Image GetIconImage()
    {
        return transform.Find("IconMask/Icon").GetComponent<Image>();
    }

    protected override Sprite GetRaritySprite(RarityType rarity)
    {
        var sprites = Resources.LoadAll<Sprite>("Card/Card_Inhand_Minion_Paladin");
        switch (rarity)
        {
            case RarityType.Normal:
                return sprites[5];
            case RarityType.Rare:
                return sprites[0];
            case RarityType.Epic:
                return sprites[3];
            case RarityType.Legend:
                return sprites[4];
            default:
                return null;
        }
    }
}
