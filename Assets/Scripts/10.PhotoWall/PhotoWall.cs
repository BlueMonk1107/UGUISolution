using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class PhotoWall : MonoBehaviour
{
    public RectTransform Prefab;
    private List<PhotoWallItem> _items;
    private List<int> _centerId; 

    private Vector2 _offset = new Vector2(10,10);

    private void Start()
    {
        _items = new List<PhotoWallItem>();
        _centerId = new List<int>();

        Reenter reenter = gameObject.AddComponent<Reenter>();
        reenter.Init(_items, _centerId);

        SpawnItems();
    }

    private void SpawnItems()
    {
        int row = Mathf.FloorToInt(Screen.height/(Prefab.rect.height + _offset.y));
        int column =  Mathf.FloorToInt(Screen.width/(Prefab.rect.width + _offset.x));
        int totalNum = row*column;
        Vector2 firstPos = new Vector2(Screen.width + Prefab.rect.width*0.5f,- Prefab.rect.height *0.5f);

        int index = 0;
        PhotoWallItem itemTemp;
        Vector2 targetPos;
        for (int i = 0; i < row; i++)
        {
            float curX = firstPos.x;
            float curY = firstPos.y - i*(Prefab.rect.height + _offset.y);

            for (int j = 0; j < column; j++)
            {
                RectTransform item = Instantiate(Prefab.gameObject, transform).GetComponent<RectTransform>();

                item.anchoredPosition = new Vector2(curX, curY);

                targetPos = new Vector2(curX - Screen.width, curY);
                item.DOAnchorPosX(targetPos.x, 1);

                itemTemp = item.gameObject.AddComponent<PhotoWallItem>();
                itemTemp.Init(index, targetPos, column, totalNum, GetItem);
                itemTemp.AddCenterListener((id) =>
                {
                    if(!_centerId.Contains(id))
                        _centerId.Add(id);
                });
                itemTemp.RemoveCenterListener((id) =>
                {
                    if (_centerId.Contains(id))
                        _centerId.Remove(id);
                });
                _items.Add(itemTemp);

                curX += Prefab.rect.width + _offset.x;
                index++;
            }

            _items[index - column].IsLeftBorder = true;
            _items[index - 1].IsRightBorder = true;
        }
    }

    private PhotoWallItem GetItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            return _items[index];
        }
        else
        {
            return null;
        }
    }
}