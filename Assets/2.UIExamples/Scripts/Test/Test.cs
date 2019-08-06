using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour,IDragHandler
{
    private int _index;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void ChangeColor()
    {
        if (_index == 0)
        {
            GetComponent<Image>().color = Color.blue;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
        _index = _index == 0 ? 1 : 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var rect = transform.GetComponent<RectTransform>();
        rect.anchoredPosition += eventData.delta;
    }
}
