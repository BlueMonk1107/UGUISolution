using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDraggingPage : Page
{
    public override void Init(Func<int, Sprite> getSprite)
    {
        base.Init(getSprite);
        SetActivevState(false);
    }

    public void BeginDragPage(Vector3 pos,Vector2 pivot)
    {
        SetActivevState(true);
        _rect.pivot = pivot;
        transform.position = pos;
        transform.eulerAngles = Vector3.zero;
    }

    public void InitShadow(Vector2 size)
    {
        Shadow.Init(size);
    }

    public void SetShadowFollow(Transform target)
    {
        Shadow.Follow(target);
    }

    public void ResetShadowData()
    {
        Shadow.ResetShadowData();
    }
}
