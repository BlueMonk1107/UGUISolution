using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpritePathHelper : MonoBehaviour
{
   
   [MenuItem("GameObject/SpritePathHelper",false,0)]
   private static void SetPath()
   {
      foreach (GameObject go in Selection.gameObjects)
      {
         foreach (Transform trans in go.transform)
         {
            if(trans.GetComponent<Image>() == null)
               continue;

            Sprite sprite = trans.GetComponent<Image>().sprite;
            string path = AssetDatabase.GetAssetPath(sprite);
            path = Application.dataPath + path.Substring(6);
            RuntimeAltasItem item = trans.GetComponent<RuntimeAltasItem>();
            if (item == null)
               item = trans.gameObject.AddComponent<RuntimeAltasItem>();

            item.Path = path;
         }
      }
   }
}
