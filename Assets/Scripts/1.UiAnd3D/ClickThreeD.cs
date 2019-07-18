using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickThreeD : MonoBehaviour, IPointerClickHandler
{
    private int _index;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        if (_index == 0)
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
        }
        else
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        }
        _index = _index == 0 ? 1 : 0;
    }
}
