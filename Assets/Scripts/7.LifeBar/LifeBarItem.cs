using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarItem : MonoBehaviour
{
    private RectTransform _rect;
    public RectTransform Rect
    {
        get
        {
            if (_rect == null)
                _rect = GetComponent<RectTransform>();
            return _rect;
        }
    }
    private Image _image;

    public Image Image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();
            return _image;
        }
    }
    private LifeBarItem _child;

    private float _defaultWidth;

    public void Init()
    {
        if (transform.Find("AdditionBar") != null)
            _child = transform.Find("AdditionBar").gameObject.AddComponent<LifeBarItem>();

        _defaultWidth = Rect.rect.width;
    }

    public void SetData(LifeBarData data)
    {
        Image.color = data.BarMainColor;
        if (data.BarSprite != null)
            Image.sprite = data.BarSprite;

        if(_child != null)
            _child.SetData(data);
    }

    public float ChangeLife(float value)
    {
        if (_child != null)
        {
            _child.DOKill();
            _child.Image.color = Image.color;
            _child.Rect.sizeDelta = Rect.sizeDelta;
            _child.Image.DOFade(0, 0.5f).OnComplete(()=>_child.ChangeLife(value));
        }

        Rect.sizeDelta += Vector2.right*value;

        return GetOutOfRange();
    }

    private float GetOutOfRange()
    {
        float offset = 0;

        if (Rect.rect.width < 0)
        {
            offset = Rect.rect.width;
            ResetToZero();
        }
        else if(Rect.rect.width > _defaultWidth)
        {
            offset = Rect.rect.width - _defaultWidth;
            ResetToWidth();
        }

        return offset;
    }

    public void ResetToZero()
    {
        Rect.sizeDelta = Vector2.zero;
    }

    public void ResetToWidth()
    {
        Rect.sizeDelta = Vector2.right*_defaultWidth;
    }
}
