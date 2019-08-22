using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShowView : MonoBehaviour
{
    private ShowItem _selectedItem;
    private RuntimeAltasItem _altasItem;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowName id = ShowName.ICON_1;
        foreach (Transform trans in transform)
        {
            RuntimeAltasItem altasItem = trans.gameObject.AddComponent<RuntimeAltasItem>();
            ShowItem item =trans.gameObject.AddComponent<ShowItem>();
            
            item.Init(id);
            id++;
            
            item.AddListener(() =>
            {
                _selectedItem = item;
                _altasItem = altasItem;
            });
        }
    }

    public void SetShowItem(Sprite sprite,string path)
    {
        _selectedItem.SetSprite(sprite);
        _altasItem.Path = path;
    }
    
    public Dictionary<string,string> GetPaths()
    {
        Dictionary<string,string> temp = new Dictionary<string, string>();
        KeyValuePair<string, string> tempPair;
        foreach (ShowItem item in GetComponentsInChildren<ShowItem>())
        {
            tempPair = item.GetData();
            temp.Add(tempPair.Key,tempPair.Value);
        }

        return temp;
    }
}
