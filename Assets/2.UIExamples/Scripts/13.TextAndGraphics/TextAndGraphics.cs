using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TextAndGraphics : Text
{
    [SerializeField]
    private List<EmojiItem> _items = new List<EmojiItem>();
    [SerializeField]
    private List<EmojiPlaceholder> _emojiStartIndex = new List<EmojiPlaceholder>();
    [SerializeField]
    private string _filterText;
    [SerializeField]
    private string _lastText;
    [SerializeField]
    private bool _lastRichText;

    public override void SetVerticesDirty()
    {
        base.SetVerticesDirty();
        
        if (_lastText != m_Text || _lastRichText != supportRichText)
        {
            _lastText = m_Text;
            _lastRichText = supportRichText;
            _filterText = FilterText(m_Text);
        }
    }

    private string FilterText(string text)
    {
        _emojiStartIndex.Clear();

        if (!supportRichText)
            return text;

        StringBuilder filterText = new StringBuilder();
        var match = EmojiConfig.EmojiRegex.Match(text);
        string placeholder = "";
        int index = 0;
        int startIndex = 0;

        while (match.Success)
        {
            startIndex = match.Index + filterText.Length;
            var item = SpawnItem(index);
            //初始化显示表情组件
            Vector2 size = InitItem(match, item);
            // 下面是 对于字符串的处理
            placeholder = string.Format("<quad width={0} height={1}/>", size.x, size.y);
            //截取匹配项前面的字符串
            filterText.Append(text.Substring(0, match.Index));
            filterText.Append(placeholder);
            text = text.Substring(match.Index + match.Length);
            match = EmojiConfig.EmojiRegex.Match(text);
            _emojiStartIndex.Add(new EmojiPlaceholder(startIndex, placeholder.Length));
            index++;
        }

        filterText.Append(text);
        return filterText.ToString();
    }

    private EmojiItem SpawnItem(int index)
    {
        if (index < _items.Count)
        {
            return _items[index];
        }
        else
        {
            GameObject itemGo = new GameObject("EmojiItem");
            itemGo.AddComponent<RectTransform>();
            itemGo.AddComponent<Image>();
            var item = itemGo.AddComponent<EmojiItem>();
            item.Init(transform);
            _items.Add(item);
            return item;
        }
    }

    private Vector2 InitItem(Match match, EmojiItem item)
    {
        string keyContent = match.Groups[0].Value;
         EmojiTypeKey type = EmojiTypeKey.S;
        string id = "";
        EmojiConfig.Instance.GetEmojiInfo(keyContent,ref type,ref id);
        Vector2 defaultSize = Vector2.zero;
        bool adpativeSize = false;

        switch (type)
        {
            case EmojiTypeKey.V:
                VideoData videoData = EmojiConfig.Instance.GetVideoData(id);
                item.PlayVideo(videoData.VideoClip);
                defaultSize = videoData.DefaultSize;
                adpativeSize = videoData.AdaptiveSize;
                break;
            case EmojiTypeKey.S:
                SpriteData spriteData = EmojiConfig.Instance.GetSpriteData(id);
                item.SetSprite(spriteData.Sprite);
                defaultSize = spriteData.DefaultSize;
                adpativeSize = spriteData.AdaptiveSize;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (adpativeSize)
        {
            defaultSize = Vector2.one*fontSize;
        }

        item.SetSize(type,defaultSize);

        return defaultSize;
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        m_DisableFontTextureRebuiltCallback = true;

        var setting = GetGenerationSettings(rectTransform.rect.size);

        cachedTextGenerator.PopulateWithErrors(_filterText, setting, gameObject);
        float unitsPerPixel = 1/pixelsPerUnit;
        SetEmojiImage(unitsPerPixel);
        AddUIVertexQuad(toFill, unitsPerPixel);
        m_DisableFontTextureRebuiltCallback = false;
    }

    private void SetEmojiImage(float unitsPerPixel)
    {
        var verts = cachedTextGenerator.verts;
        int index = 0;
        for (int i = 0; i < _items.Count; i++)
        {
            if (i < _emojiStartIndex.Count)
            {
                index = _emojiStartIndex[i].StartIndex;
                Vector2 one = verts[index*4].position;
                Vector2 two = verts[index*4 + 2].position;
                Vector2 center = (one + two)/2;

                _items[i].SetPos(center * unitsPerPixel);
                SetEmojiShowState(i, true);
            }
            else
            {
                SetEmojiShowState(i, false);
            }
        }
    }

    private void SetEmojiShowState(int index,bool show)
    {
        _items[index].SetActive(show);
    }

    private void AddUIVertexQuad(VertexHelper toFill, float unitsPerPixel)
    {
        toFill.Clear();
        var verts = cachedTextGenerator.verts;
        UIVertex[] verTemp = new UIVertex[4];

        Vector2 offset = new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
        offset = PixelAdjustPoint(offset) - offset;
        bool needOffset = offset != Vector2.zero;
        int index = 0;

        for (int i = 0; i < _filterText.Length; i++)
        {
            if (index < _emojiStartIndex.Count && _emojiStartIndex[index].StartIndex == i)
            {
                i += _emojiStartIndex[index].Length - 1;
                index ++;
                continue;
            }

            for (int j = 0; j < 4; j++)
            {
                verTemp[j] = verts[i*4 + j];
                verTemp[j].position *= unitsPerPixel;
                if (needOffset)
                {
                    verTemp[j].position.x += offset.x;
                    verTemp[j].position.y += offset.y;
                }
            }

            toFill.AddUIVertexQuad(verTemp);
        }
    }
}

[Serializable]
public class EmojiPlaceholder
{
    public int StartIndex;
    public int Length;

    public EmojiPlaceholder(int startIndex, int length)
    {
        StartIndex = startIndex;
        Length = length;
    }
}


