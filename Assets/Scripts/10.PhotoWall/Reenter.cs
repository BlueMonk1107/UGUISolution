using System.Collections.Generic;
using UnityEngine;

public class Reenter : MonoBehaviour
{
    private bool _isMove;
    private List<PhotoWallItem> _items;
    private List<int> _centerId;
    private Vector2 _itemPos;
    private Vector2 _changePos;

    public void Init(List<PhotoWallItem> items, List<int> centerId)
    {
        _isMove = false;
        _items = items;
        _centerId = centerId;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            InitMove();
        }

        if (_isMove)
        {
            _isMove = !Move();
        }
    }

    private void InitMove()
    {
        _isMove = true;
        SetState();
        ResetPos();
    }

    private void ResetPos()
    {
        foreach (PhotoWallItem item in _items)
        {
            if (item.IsCenter || !item.CanMove)
                continue;

            item.Rect.anchoredPosition += Vector2.right * Screen.width;
        }
    }

    private void SetState()
    {
        Vector2 itemPos;
        Vector2 centerPos;
        foreach (var i in _centerId)
        {
            centerPos = _items[i].Rect.anchoredPosition;
            foreach (PhotoWallItem item in _items)
            {
                itemPos = item.Rect.anchoredPosition;
                if (itemPos.x < centerPos.x
                    && itemPos.y < centerPos.y + item.Radius
                    && itemPos.y > centerPos.y - item.Radius
                    && item.CanMove)
                {
                    item.IsBehindCenterPoint = true;

                    if (!item.CenterPoints.Contains(centerPos))
                        item.CenterPoints.Add(centerPos);

                    item.CircleXMax.Add(centerPos, GetXMax(centerPos, item.TargetPos.y, item.Radius));
                    item.CircleXMin.Add(centerPos, GetXMin(centerPos, item.TargetPos.y, item.Radius));
                }
            }
        }
    }

    private bool Move()
    {
        bool isEnd = true;
        foreach (PhotoWallItem item in _items)
        {
            if (item.IsCenter || !item.CanMove)
                continue;

            if (item.Rect.anchoredPosition.x > item.TargetPos.x)
            {
                isEnd = false;
                _itemPos = item.Rect.anchoredPosition + Vector2.left * 100;

                if (item.IsBehindCenterPoint)
                {
                    _changePos = Vector2.zero;
                    foreach (Vector2 center in item.CenterPoints)
                    {
                        if (_itemPos.x <= item.CircleXMax[center]
                         && _itemPos.x >= item.CircleXMin[center])
                        {
                            _changePos.x = _itemPos.x;
                            if (_itemPos.y >= center.y)
                            {
                                _changePos.y = GetYMax(center, _itemPos.x, item.Radius);
                            }
                            else
                            {
                                _changePos.y = GetYMin(center, _itemPos.x, item.Radius);
                            }
                        }

                        if (_changePos != Vector2.zero)
                        {
                            _itemPos = _changePos;
                            break;
                        }
                    }
                }

                if (_changePos == Vector2.zero)
                {
                    _itemPos.y = item.TargetPos.y;
                }

                item.Rect.anchoredPosition = _itemPos;
            }
            else
            {
                item.ResetMoveData();
            }
        }

        return isEnd;
    }

    private float GetYMin(Vector2 center, float x, float radius)
    {
        return center.y - Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - center.x, 2));
    }

    private float GetYMax(Vector2 center, float x, float radius)
    {
        return center.y + Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - center.x, 2));
    }

    private float GetXMin(Vector2 center, float y, float radius)
    {
        return center.x - Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(y - center.y, 2));
    }

    private float GetXMax(Vector2 center, float y, float radius)
    {
        return center.x + Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(y - center.y, 2));
    }
}
