//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEditor;
//using System.Linq;
//using System;
//using System.Collections.Generic;
//using UnityEngine.UI;
//
//[InitializeOnLoad]
//public class CustomHierarchy
//{
//    // 总的开关用于开启或关闭 显示图标以及彩色文字
//    public static bool EnableCustomHierarchy = true;
//    public static bool EnableCustomHierarchyLabel = true;
//
//    static CustomHierarchy()
//    {
//        EditorApplication.hierarchyWindowItemOnGUI += HierarchWindowOnGui;
//    }
//
//    // 绘制Hiercrch
//    static void HierarchWindowOnGui(int instanceId, Rect selectionRect)
//    {
//        if (!EnableCustomHierarchy) return;
//        try
//        {
//
//            // 通过ID获得Obj
//            var obj = EditorUtility.InstanceIDToObject(instanceId);
//            var go = (GameObject)obj;// as GameObject;
//            GUIStyle style = null;
//
//            // 文字颜色定义 
//            var colorLight = new Color(251/255f, 244/255f, 124/255f);
//
//            
//            // 绘制新的Label覆盖原有名字
//            if (EnableCustomHierarchyLabel)
//            {
//                // 字体样式
//                style = LabelStyle(colorLight);
//            }
//            // 绘制Label来覆盖原有的名字
//            if (style != null && go.activeInHierarchy)
//            {
//                GUI.Label(selectionRect, go.name, style);
//                Image image = go.GetComponent<Image>();
//                if (image != null)
//                {
//                    Debug.Log(go.name+"  "+image.depth);
//                }
//                
//                Text text = go.GetComponent<Text>();
//                if (text != null)
//                {
//                    Debug.Log(go.name+"  "+text.depth);
//                }
//            }
//        }
//        catch (Exception)
//        {
//        }
//    }
//    
//    // 用于覆盖原有文字的LabelStyle
//    private static GUIStyle LabelStyle(Color color)
//    {
//        var style = new GUIStyle(((GUIStyle) "Label"))
//        {
//            padding =
//            {
//                left = EditorStyles.label.padding.left+41,
//                top = EditorStyles.label.padding.top
//            },
//            normal =
//            {
//                textColor = color
//            }
//        };
//        return style;
//    }
//}