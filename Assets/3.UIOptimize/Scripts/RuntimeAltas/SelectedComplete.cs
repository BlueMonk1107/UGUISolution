using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedComplete : MonoBehaviour
{
    // Start is called before the first frame update
    public void Init(Func<Dictionary<string,string>> getPaths,LoadingView loadingView)
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            loadingView.SetActiveState(true);
            AssetPackerMgr.Instance.GentatorNewAltas("Test",getPaths(),()=>loadingView.SwitchScene(SceneName.Game.ToString()));
        });
    }
}

public enum SceneName
{
    SelectView,
    Game
}
