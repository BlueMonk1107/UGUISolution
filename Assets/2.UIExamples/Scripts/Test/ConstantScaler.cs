using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstantScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float wScale = Screen.width / 1920.0f;
        float hScale = Screen.height / 1080.0f;

        GetComponent<CanvasScaler>().scaleFactor = wScale;
    }
}
