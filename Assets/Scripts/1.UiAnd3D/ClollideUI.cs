using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClollideUI : MonoBehaviour,IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeColor();
        //ExecuteAll(eventData);
    }

    public void ExecuteAll(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject != gameObject)
            {
                ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
