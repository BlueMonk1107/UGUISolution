using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AutoAddLanguageItem
{
    [InitializeOnLoadMethod]
    private static void Init()
    {
        EditorApplication.hierarchyChanged += ChangeLaguage;
    }

    private static void ChangeLaguage()
    {
        GameObject go = Selection.activeGameObject;
        if (go != null)
        {
            Text text = go.GetComponent<Text>();
            if (text != null && text.GetComponent<LanguageItem>() == null)
            {
                go.AddComponent<LanguageItem>();
            }
        }
    }
}
