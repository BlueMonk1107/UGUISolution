using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Language language = Language.Language_CN;
            switch (MultiLanguageSystem.Instance.CurentLanguage)
            {
                case Language.Language_CN:
                    language = Language.Language_EN;
                    break;
                case Language.Language_EN:
                    language = Language.Language_CN;
                    break;
            }

            MultiLanguageSystem.Instance.SetLanguage(language);
        });
    }
}
