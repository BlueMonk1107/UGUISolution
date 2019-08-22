using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().DOText("Loading...", 3).SetLoops(-1, LoopType.Restart);
    }

    public void SetActiveState(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string scenceName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scenceName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2);
                async.allowSceneActivation = true;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
