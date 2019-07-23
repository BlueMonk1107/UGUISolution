using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCard : CardBase
{
    protected override Image GetIconImage()
    {
        return transform.Find("Icon").GetComponent<Image>();
    }

    protected override Sprite GetRaritySprite(RarityType rarity)
    {
        var sprites = Resources.LoadAll<Sprite>("Card/Card_Inhand_Ability_Druid");
        switch (rarity)
        {
            case RarityType.Normal:
                return sprites[4];
            case RarityType.Rare:
                return sprites[0];
            case RarityType.Epic:
                return sprites[2];
            case RarityType.Legend:
                return sprites[3];
            default:
                return null;
        }
    }
}
