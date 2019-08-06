using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    private Func<int, Sprite> _getSprite;
    private Image _image;
    private int _id;
    public Shadow Shadow { get; private set; }
    protected RectTransform _rect;

    public int ID
    {
        get { return _id; }
        set
        {
            _id = value;
            ChangeSprite(value);
        }
    }

    public virtual void Init(Func<int,Sprite> getSprite)
    {
        _rect = GetComponent<RectTransform>();
        _getSprite = getSprite;
        _image = GetComponent<Image>();
        Shadow = transform.GetChild(0).gameObject.AddComponent<Shadow>();
    }

    private void ChangeSprite(int id)
    {
        _image.sprite = _getSprite(id);
    }

    public void SetActivevState(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetParent(Transform parent,bool worldPosStays)
    {
        transform.SetParent(parent, worldPosStays);
    }
}
