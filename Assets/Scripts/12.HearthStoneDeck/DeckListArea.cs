using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckListArea : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Action _enter;
    private Action _exit;

    public void Init(Action enter, Action exit)
    {
        _enter = enter;
        _exit = exit;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enter != null && eventData.pointerEnter!= null && eventData.dragging)
            _enter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exit != null && eventData.pointerEnter != null && eventData.dragging)
            _exit();
    }
}
