using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectView : MonoBehaviour
{
    private Action<Sprite, string> _onSelected;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var child in GetComponentsInChildren<Button>())
        {
            child.onClick.AddListener(() =>
            {
                Sprite sprite = child.GetComponent<Image>().sprite;
                RuntimeAltasItem item = child.GetComponent<RuntimeAltasItem>();

                if (_onSelected != null)
                    _onSelected(sprite, item.Path);
            });
        }
    }

    public void AddSelectedListener(Action<Sprite, string> selected)
    {
        _onSelected = selected;
    }
}
