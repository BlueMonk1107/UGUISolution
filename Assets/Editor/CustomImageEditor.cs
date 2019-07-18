using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CustomImageEditor : Editor
{
    private const int UI_LAYER = 5;

    [MenuItem("GameObject/UI/CustomImage", priority = 0)]
    private static void AddImage()
    {

        Transform canvasTrans = GetCanvasTrans();

        Transform image = AddCustomImage();

        if (Selection.activeGameObject!= null && Selection.activeGameObject.layer == UI_LAYER)
        {
            image.SetParent(Selection.activeGameObject.transform);
        }
        else
        {
            image.SetParent(canvasTrans);
        }
       
        image.localPosition = Vector3.zero;
    }

    private static Transform GetCanvasTrans()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            SetLayer(canvasObj);
            canvasObj.AddComponent<RectTransform>();
            canvasObj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            return canvasObj.transform;
        }
        else
        {
            return canvas.transform;
        }
    }

    private static Transform AddCustomImage()
    {
        GameObject image = new GameObject("Image");
        SetLayer(image);
        image.AddComponent<RectTransform>();
        image.AddComponent<PolygonCollider2D>();
        image.AddComponent<CustomImage>();

        return image.transform;
    }

    private static void SetLayer(GameObject ui)
    {
        ui.layer = UI_LAYER;
    }
}
