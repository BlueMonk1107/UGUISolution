using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoItem : MonoBehaviour
{
    [SerializeField]
    private RenderTexture _rt;
    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private RectTransform RectTrans;
    [SerializeField]
    private RawImage _rawImage;

    public void Init(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;

        AddComponent(ref _videoPlayer);
        AddComponent(ref RectTrans);
        AddComponent(ref _rawImage);

        _rt = new RenderTexture(1280,720,0);
        _rawImage.texture = _rt;

        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        _videoPlayer.targetTexture = _rt;
        _videoPlayer.isLooping = true;
        _videoPlayer.SetDirectAudioVolume(0,0.2f);
    }

    private void AddComponent<T>(ref T component) where T : Component
    {
        component = GetComponent<T>();
        if (component == null)
            component = gameObject.AddComponent<T>();
    }


    public void Play(VideoClip clip)
    {
        _videoPlayer.clip = clip;
        _videoPlayer.Play();
    }

    public void SetSize(Vector2 size)
    {
        RectTrans.sizeDelta = size;
    }

    public void SetActive(bool show)
    {
        _rawImage.canvasRenderer.cull = !show;
        if (show)
        {
            _rawImage.Rebuild(CanvasUpdate.PreRender);
        }

        if (!show)
        {
            _videoPlayer.Stop();
            _videoPlayer.clip = null;
        }
    }
}
