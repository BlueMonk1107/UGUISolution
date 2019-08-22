using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GetSpriteTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Atlas/TestAtlas");
        GetComponent<Image>().sprite = atlas.GetSprite("boot01");
    }
    
}
