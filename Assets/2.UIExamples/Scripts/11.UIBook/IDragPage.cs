using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragPage
{
    void BeginDragPage(Vector3 point);

    void UpdateDrag();

    void EndDragPage(Action complete);
}

public abstract class DragPageBase
{
    private UIBook _book;
    protected BookModel _bookModel;
    private TheDraggingPage _frontPage;
    private TheDraggingPage _backPage;
    private Vector3 _startPos;
    private RectTransform _clippingMask;

    public DragPageBase(UIBook book, BookModel model, TheDraggingPage frontPage, TheDraggingPage backPage, Vector3 startPos)
    {
        _book = book;
        _bookModel = model;
        _frontPage = frontPage;
        _backPage = backPage;
        _startPos = startPos;
        _clippingMask = book.GetClippingMask();
    }

    public void BeginDragPage(Vector3 point)
    {
        _clippingMask.pivot = GetClippingMaskPivot(); 

        _bookModel.ClickPoint = point;

        _frontPage.BeginDragPage(_startPos, GetPagePivot());
        _backPage.BeginDragPage(_startPos, GetPagePivot());
    }

    protected abstract Vector2 GetClippingMaskPivot();
    protected abstract Vector2 GetPagePivot();

    public void UpdateDrag()
    {
        _bookModel.ClickPoint = _book.GetClickPos();

        _backPage.SetParent(_clippingMask, true);
        _frontPage.SetParent(_book.transform, true);

        _bookModel.CurrentPageCorner = _book.CulculateDraggingCorner(_bookModel.ClickPoint);

        Vector3 bottomCrossPoint = UpdateClippingMask();

        UpdateBackSide(bottomCrossPoint);

        _frontPage.SetParent(_clippingMask, true);
        _frontPage.ResetShadowData();
        _frontPage.transform.SetAsFirstSibling();

        _backPage.SetShadowFollow(_clippingMask);
    }

    private Vector3 UpdateClippingMask()
    {
        Vector3 bottomCrossPoint;
        float angle = _book.CulculateFoldAngle(_bookModel.CurrentPageCorner, GetBookCorner(), out bottomCrossPoint);

        if (angle > 0)
        {
            angle = angle - 90;
        }
        else
        {
            angle = angle + 90;
        }

        _clippingMask.eulerAngles = Vector3.forward * angle;
        _clippingMask.localPosition = bottomCrossPoint;

        return bottomCrossPoint;
    }

    protected abstract Vector3 GetBookCorner();

    private void UpdateBackSide(Vector3 bottomCrossPoint)
    {
        _backPage.transform.position = _book.Local2WorldPos(_bookModel.CurrentPageCorner);
        Vector3 offset = bottomCrossPoint - _bookModel.CurrentPageCorner;

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        _backPage.transform.eulerAngles = GetValidAngle(angle) ;
    }

    protected abstract Vector3 GetValidAngle(float angle);

    public void EndDragPage(Action complete)
    {
        Vector3 corner;
        if (_bookModel.CurrentPageCorner.x > _bookModel.BottomCenter.x)
        {
            corner = _bookModel.RightCorner;
        }
        else
        {
            corner = _bookModel.LeftCorner;
        }

        _book.FlipAni(corner, complete);
    }
}
