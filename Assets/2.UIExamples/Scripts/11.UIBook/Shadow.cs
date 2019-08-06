using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    private RectTransform _rect;
    private Image _image;
    private Color _defaultColor = new Color(1,1,1,0.5f);

    public void Init(Vector2 size)
    {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _rect.sizeDelta = size;

        _image.color = _defaultColor;
    }

    public void Follow(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public void ResetShadowData()
    {
        _rect.anchoredPosition = Vector2.zero;
        _rect.localEulerAngles = Vector3.zero;
    }
}
