using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LoopListItemModel
{
    public Sprite Icon;
    public string Describe;


    public LoopListItemModel(Sprite icon, string describe)
    {
        Icon = icon;
        Describe = describe;
    }
}
