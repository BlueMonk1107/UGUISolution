using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatchTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int totalLength = 0;
        Text text = GetComponent<Text>();
        Font myFont = text.font;  //chatText is my Text component
        myFont.RequestCharactersInTexture(text.text, text.fontSize, text.fontStyle);
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = text.text.ToCharArray();
 
        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, text.fontSize);

            totalLength += characterInfo.advance;
        }
        
        Debug.Log(totalLength+"  "+text.preferredWidth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
