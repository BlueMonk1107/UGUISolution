using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Video;

public class EmojiConfig 
{
    private static EmojiConfig _instance;
    private Dictionary<string, SpriteData> _sprites = new Dictionary<string, SpriteData>(); 

    public static EmojiConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EmojiConfig();
                _instance.Init();
            }

            return _instance;
        }
    }

    public const string DEFAULT_PATH = "Emoji/";

    private AssetData[] SpriteData =
    {
        new AssetData("EmojiAtlas", new Vector2(30, 30), true, false),
    };

    private AssetData[] VideoData =
   {
        new AssetData("TestVideo", new Vector2(30, 30), false, false),
    };

    /// <summary>
    /// 表情标识符
    /// </summary>
    public const char IDENTIFIERS = '$';
    /// <summary>
    /// 关键字分隔符
    /// </summary>
    public const char SEPARATOR = '&';
    public static readonly Regex EmojiRegex = new Regex(string.Format("\\{0}[A-Z]{1}[0-9]+\\{2}", IDENTIFIERS, SEPARATOR, IDENTIFIERS));

    public void GetEmojiInfo(string ketContent,ref EmojiTypeKey type,ref string spriteIndex)
    {
        ketContent = ketContent.Substring(1, ketContent.Length - 2);
        var keys = ketContent.Split(SEPARATOR);

        for (EmojiTypeKey i = 0; i < EmojiTypeKey.COUNT; i++)
        {
            if (i.ToString() == keys[0])
            {
                type = i;
                break;
            }
        }

        spriteIndex = keys[1];
    }

    public void Init()
    {
        foreach (AssetData data in SpriteData)
        {
            if (data.IsAtlas)
            {
                var sprites = Resources.LoadAll<Sprite>(data.Path);
                foreach (Sprite sprite in sprites)
                {
                    SaceSpriteData(sprite, data);
                }
            }
            else
            {
                var sprite = Resources.Load<Sprite>(data.Path);
                SaceSpriteData(sprite, data);
            }
        }
    }

    private void SaceSpriteData(Sprite sprite, AssetData data)
    {
        SpriteData dataTemp = new SpriteData();
        dataTemp.Sprite = sprite;
        dataTemp.DefaultSize = data.DefaultSize;
        dataTemp.AdaptiveSize = data.AdaptiveSize;
        _sprites[_sprites.Count.ToString()] = dataTemp;
    }

    public SpriteData GetSpriteData(string id)
    {
        if (_sprites.ContainsKey(id))
        {
            return _sprites[id];
        }
        else
        {
            return null;
        }
    }

    public VideoData GetVideoData(string id)
    {
        int index = int.Parse(id);
        AssetData data = VideoData[index];

        VideoData tempData = new VideoData();
        tempData.VideoClip = Resources.Load<VideoClip>(data.Path);
        tempData.DefaultSize = data.DefaultSize;
        tempData.AdaptiveSize = false;

        return tempData;
    }
}

public enum EmojiTypeKey
{
    /// <summary>
    /// 视频
    /// </summary>
    V,
    /// <summary>
    /// 音频
    /// </summary>
    A,
    /// <summary>
    /// 图片
    /// </summary>
    S,
    /// <summary>
    /// 用来标记当前枚举的数量
    /// </summary>
    COUNT
}

public class SpriteData
{
    public Sprite Sprite { get; set; }
    public Vector2 DefaultSize { get; set; }
    public bool AdaptiveSize { get; set; }
}

public class VideoData
{
    public VideoClip VideoClip { get; set; }
    public Vector2 DefaultSize { get; set; }
    public bool AdaptiveSize { get; set; }
}

public class AssetData
{
    public string Path { get; private set; }
    public Vector2 DefaultSize { get; private set; }
    public bool IsAtlas { get; private set; }
    public bool AdaptiveSize { get; private set; }

    public AssetData(string path, Vector2 size, bool isAtlas, bool adaptiveSize)
    {
        Path = EmojiConfig.DEFAULT_PATH + path;
        DefaultSize = size;
        IsAtlas = isAtlas;
        AdaptiveSize = adaptiveSize;
    }
}
