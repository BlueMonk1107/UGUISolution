using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowView : MonoBehaviour
{
    private SelectShowView _selectShowView;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<SelectView>().AddSelectedListener(SetShowItem);
        _selectShowView = GetComponentInChildren<SelectShowView>();
        GetComponentInChildren<SelectedComplete>().Init(
            ()=>_selectShowView.GetPaths(),
            GetComponentInChildren<LoadingView>(true));
    }

    private void SetShowItem(Sprite sprite,string path)
    {
        if(_selectShowView == null)
            return;
        
        _selectShowView.SetShowItem(sprite,path);
    }

  
}
