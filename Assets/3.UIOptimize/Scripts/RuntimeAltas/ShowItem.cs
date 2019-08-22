using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItem : MonoBehaviour
{
    public ShowName ID { get; private set; }
    private Image _image;
    private RuntimeAltasItem _altasItem;

    public void Init(ShowName id)
    {
        ID = id;
        _image = GetComponent<Image>();
        _altasItem = GetComponent<RuntimeAltasItem>();
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void AddListener(Action selected)
    {
        gameObject.GetComponent<Button>().onClick.AddListener(()=>selected());
    }

    public KeyValuePair<string, string> GetData()
    {
        return new KeyValuePair<string, string>(ID.ToString(),_altasItem.Path);
    }
}

public enum ShowName
{
    ICON_1,
    ICON_2,
    ICON_3,
}
