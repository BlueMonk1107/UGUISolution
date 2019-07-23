using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service
{
    private static Service _instance;
    public static Service Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Service();

            return _instance;
        }
    }

    public List<CardModel> GetCardsData()
    {
        List<CardModel> cards = new List<CardModel>();
        cards.Add(GetCardData("软泥怪", "AcidicSwampOoze_D", CardType.MinionCard, RarityType.Normal, 2, 3, 2));
        cards.Add(GetCardData("大法师安东尼达斯", "Antonidas_D", CardType.MinionCard, RarityType.Legend, 7, 5, 7));
        cards.Add(GetCardData("工程师学徒", "W7_078_D", CardType.MinionCard, RarityType.Normal, 2, 1, 1));
        cards.Add(GetCardData("藏宝海湾保镖", "W7_183_D", CardType.MinionCard, RarityType.Normal, 5, 5, 4));
        cards.Add(GetCardData("剃刀猎手", "W8_018_D", CardType.MinionCard, RarityType.Normal, 3, 2, 3));
        cards.Add(GetCardData("精灵弓箭手", "W10_A109_D", CardType.MinionCard, RarityType.Normal, 1, 1, 1));
        cards.Add(GetCardData("奥术射击", "W10_A198_D_Mask", CardType.MagicCard, RarityType.Normal, 2, 0, 0));
        return cards;
    }

    private CardModel GetCardData(string name, string spriteName, CardType cardType, RarityType rarityType, int cost, int attack, int life)
    {
        CardModel model = new CardModel();
        model.Name = name;
        model.SpriteName = spriteName;
        model.Type = (int)cardType;
        model.RarityType = (int)rarityType;
        model.Cost = cost;
        model.Attack = attack;
        model.Life = life;
        return model;
    }
}
