using System.Collections;
using System.Collections.Generic;
using Multi_Language;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LanguageItem : MonoBehaviour
{
    [SerializeField]
    private int ID;
    private Text _text;
    private readonly string _fontsPath = "Fonts/";

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        MultiLanguageSystem.Instance.AddUpdateListener(UpdateData);
        UpdateData();
    }

    private void UpdateData()
    {
        TextData textData = MultiLanguageSystem.Instance.GetTextData(ID);
        if (textData.Equals(default(TextData)))
        {
            Debug.LogError("未获取到正确数据id为：" + ID);
            return;
        }

        _text.text = textData.Content;

        StyleData styleData = MultiLanguageSystem.Instance.GetStyleData(textData.Style);
        if (styleData.Equals(default(StyleData)))
        {
            Debug.LogError("未获取到正确风格数据，风格名称为：" + textData.Style);
            return;
        }

        _text.font = LoadFont(styleData.Font);
        _text.fontStyle = GetFontStyle(styleData.FontStyle);
        _text.fontSize = styleData.Size;
        _text.supportRichText = styleData.RichText;
        _text.color = new Color32(styleData.r, styleData.g, styleData.b, styleData.a);
    }

    private Font LoadFont(string fontName)
    {
        if (_text.font.name == fontName)
            return _text.font;

        Font font = Resources.Load<Font>(_fontsPath + fontName);
        if (font != null)
        {
            return font;
        }
        else
        {
            Debug.LogError("加载字体不存在，字体名称为：" + fontName);
            return _text.font;
        }
    }

    private FontStyle GetFontStyle(string style)
    {
        if (style == CustomFontStyle.B.ToString())
        {
            return FontStyle.Bold;
        }
        else if (style == CustomFontStyle.I.ToString())
        {
            return FontStyle.Italic;
        }
        else if (style == CustomFontStyle.ALL.ToString())
        {
            return FontStyle.BoldAndItalic;
        }
        else
        {
            return FontStyle.Normal;
        }
    }
}

public enum CustomFontStyle
{
    N,
    B,
    I,
    ALL
}
