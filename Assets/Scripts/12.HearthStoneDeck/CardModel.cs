using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardModel
{
    public int Cost { get; set; }
    public string Name { get; set; }
    public string SpriteName { get; set; }
    public int Type { get; set; }
    public int Attack { get; set; }
    public int Life { get; set; }
    public int RarityType { get; set; }
}

public enum SizeType
{
    NormalCard,
    MiniCard
}

public enum CardType
{
    MagicCard,
    MinionCard,
}

public enum RarityType
{
    Normal,
    Rare,
    Epic,
    Legend
}
