using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    CardModel Model { get; }

    CardType Type { get; }

    SizeType Size { get; }

    void Init(CardModel model);

    void SetRaycastState(bool isReceive);
}
