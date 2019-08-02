using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Multi_Language;
using UnityEngine;

public class MultiLanguageSystem
{
    private static MultiLanguageSystem _instance;

    public static MultiLanguageSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MultiLanguageSystem();
                _instance.Init();
            }

            return _instance;
        }
    }

    public Language CurentLanguage { get; private set; }

    private Dictionary<int, TextData> _textDatas;
    private Dictionary<string, StyleData> _styleDatas;
    private readonly string _configPath = Application.streamingAssetsPath + "/LanguageConfig/";
    private readonly string _styleConfigName = "Style";
    private Action _onUpdate;

    public void Init()
    {
        _textDatas = new Dictionary<int, TextData>();
        _styleDatas = new Dictionary<string, StyleData>();
        ReadStyleConfig(_configPath+ _styleConfigName);
        CurentLanguage = GetDefaultLanguage();
        SetLanguage(CurentLanguage);
    }

    private Language GetDefaultLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                return Language.Language_CN;
            default:
                return Language.Language_EN;
        }
    }

    public void SetLanguage(Language language)
    {
        CurentLanguage = language;
        ReadLaguageConfig(_configPath + language);
        if(_onUpdate != null)
            _onUpdate();
    }

    public void AddUpdateListener(Action update)
    {
        _onUpdate += update;
    }

    private void ReadLaguageConfig(string path)
    {
        _textDatas.Clear();
        using (var stream = new FileStream(path,FileMode.Open,FileAccess.Read))
        {
            using (var reader = new BinaryReader(stream))
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    TextData data = new TextData();
                    data.ID = reader.ReadInt32();
                    data.Style = reader.ReadString();
                    data.Content = reader.ReadString();
                    _textDatas.Add(data.ID,data);
                }
            }
        }
    }

    private void ReadStyleConfig(string path)
    {
        _styleDatas.Clear();
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new BinaryReader(stream))
            {
                string name = "";
                int count = int.Parse(reader.ReadString());
                for (int i = 0; i < count; i++)
                {
                    StyleData data = new StyleData();
                    name = reader.ReadString();
                    data.Font = reader.ReadString();
                    data.FontStyle = reader.ReadString();
                    data.Size = int.Parse(reader.ReadString());
                    data.RichText = reader.ReadString().Contains("True") ;
                    data.r = byte.Parse(reader.ReadString());
                    data.g = byte.Parse(reader.ReadString());
                    data.b = byte.Parse(reader.ReadString());
                    data.a = byte.Parse(reader.ReadString());
                    _styleDatas.Add(name,data);
                }
            }
        }
    }

    public TextData GetTextData(int id)
    {
        if (_textDatas.ContainsKey(id))
        {
            return _textDatas[id];
        }
        else
        {
            Debug.LogError("不存在id:"+id);
            return default(TextData);
        }
    }

    public StyleData GetStyleData(string name)
    {
        if (_styleDatas.ContainsKey(name))
        {
            return _styleDatas[name];
        }
        else
        {
            Debug.LogError("不存在文本风格:" + name);
            return default(StyleData);
        }
    }
}

public enum Language
{
    Language_CN,
    Language_EN
}

