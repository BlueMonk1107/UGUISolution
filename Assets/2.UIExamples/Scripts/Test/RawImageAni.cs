using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageAni : MonoBehaviour
{
    private RawImage _rawImage;
    private float _offsetX;
    private float _offsetY;

    // Start is called before the first frame update
    void Start()
    {
        _rawImage = GetComponent<RawImage>();
        _offsetX = 1/4.0f;
        _offsetY = 1/2.0f;
        StartCoroutine(Ani());
    }

    private IEnumerator Ani()
    {
        float x = 0;
        float y = 0;
        while (true)
        {
            y += _offsetY;
            while (x < 1)
            {
                x += _offsetX;
                _rawImage.uvRect = new Rect(x,y, _rawImage.uvRect.width, _rawImage.uvRect.height);
                yield return new WaitForSeconds(0.3f);
            }
            x = 0;
        }
    }
}
