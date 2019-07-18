using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollideCube : MonoBehaviour
{
    private int _index;
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
    }

    void OnMouseDown()
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
