using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIcon : MonoBehaviour
{
    public ShowName Name;
    // Start is called before the first frame update
    void Start()
    {
        var altas = AssetPackerMgr.Instance.GetAltas("Test");
        GetComponent<Image>().sprite = altas.GetSprite(Name.ToString());
    }
}
