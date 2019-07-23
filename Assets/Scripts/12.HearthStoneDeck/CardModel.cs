using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardModel
{
    /// <summary>
    /// 卡牌费用
    /// </summary>
    public int Cost { get; set; }
    /// <summary>
    /// 卡牌名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 卡牌图片资源名称
    /// </summary>
    public string SpriteName { get; set; }
    /// <summary>
    /// 卡牌类型
    /// </summary>
    public int Type { get; set; }
    /// <summary>
    /// 攻击
    /// </summary>
    public int Attack { get; set; }
    /// <summary>
    /// 生命
    /// </summary>
    public int Life { get; set; }
    /// <summary>
    /// 稀有度
    /// </summary>
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