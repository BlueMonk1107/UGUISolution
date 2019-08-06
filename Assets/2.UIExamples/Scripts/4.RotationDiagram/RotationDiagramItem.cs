using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotationDiagramItem : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public int PosId;
    private Action<float> _moveAction;
    private Image _image;
    private float _offsetX;
    private float _aniTime = 1;

    private Image Image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();

            return _image;
        }
    }

    private RectTransform _rect;

    private RectTransform Rect
    {
        get
        {
            if (_rect == null)
                _rect = GetComponent<RectTransform>();

            return _rect;
        }
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetSprite(Sprite sprite)
    {
        Image.sprite = sprite;
    }

    public void SetPosData(ItemPosData data)
    {
        Rect.DOAnchorPos(Vector2.right*data.X, _aniTime);
        Rect.DOScale(Vector3.one*data.ScaleTimes, _aniTime);
        
        StartCoroutine(Wait(data));
    }

    private IEnumerator Wait(ItemPosData data)
    {
        yield return new WaitForSeconds(_aniTime*0.5f);
        transform.SetSiblingIndex(data.Order);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _offsetX += eventData.delta.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _moveAction(_offsetX);
        _offsetX = 0;
    }

    public void AddMoveListener(Action<float> onMove)
    {
        _moveAction = onMove;
    }

    public void ChangeId(int symbol,int totalItemNum)
    {
        int id = PosId;
        id += symbol;
        if (id < 0)
        {
            id += totalItemNum;
        }
        PosId = id%totalItemNum;
    }
}
