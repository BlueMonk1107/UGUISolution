using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLeftPage : DragPageBase
{
    public DragLeftPage(UIBook          book, 
                        BookModel       model, 
                        TheDraggingPage frontPage, 
                        TheDraggingPage backPage, 
                        Vector3        startPos) 
                        : base(book, model, frontPage, backPage, startPos)
    {
    }

    protected override Vector2 GetClippingMaskPivot()
    {
        return new Vector2(0, _bookModel.ClippingPivotY);
    }

    protected override Vector2 GetPagePivot()
    {
        return Vector2.right;
    }

    protected override Vector3 GetBookCorner()
    {
        return _bookModel.LeftCorner;
    }

    protected override Vector3 GetValidAngle(float angle)
    {
        return Vector3.forward * (angle +180);
    }
}
