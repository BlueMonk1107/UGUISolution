using System.Collections;
using System.Collections.Generic;

public interface ICard
{
    CardType CardType { get;}

    CardModel Model { get; }

    SizeType SizeType { get; }

    void Init(SizeType size,CardModel model);
}