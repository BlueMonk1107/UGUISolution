using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EmojiItem : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private RectTransform _rect;
    [SerializeField]
    private VideoItem _videoItem;

    public void Init(Transform parent)
    {
       transform.SetParent(parent);
        _image = GetComponent<Image>();
        _rect = GetComponent<RectTransform>();
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void PlayVideo(VideoClip clip)
    {
        if (_videoItem == null)
        {
            GameObject video = new GameObject("Video");
            video.AddComponent<RectTransform>();
            _videoItem = video.AddComponent<VideoItem>();
            _videoItem.Init(transform);
        }

        _videoItem.Play(clip);
    }

    public void SetSize(EmojiTypeKey type, Vector2 size)
    {
        switch (type)
        {
            case EmojiTypeKey.V:
                _videoItem.SetSize(size);
                break;
            case EmojiTypeKey.S:
                _rect.sizeDelta = size;
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }

    public void SetPos(Vector2 pos)
    {
        transform.localPosition = pos;
    }

    public void SetActive(bool show)
    {
        _image.canvasRenderer.cull = !show;
        if (show)
        {
            _image.Rebuild(CanvasUpdate.PreRender);
        }

        if(_videoItem != null)
            _videoItem.SetActive(show);
    }

}
