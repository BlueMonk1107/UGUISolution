using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    private int _id;
    private int _centerId, _lastCenterId;
    private float _radius;
    private PhotoWallItem _item;
    private Image _image;
    private Func<int, PhotoWallItem> _getItem;
    private CanvasGroup _group;
    public bool CanMove { get; private set; }
    public void Init(int id,float radius, PhotoWallItem item,Func<int, PhotoWallItem> getItem)
    {
        _id = id;
        _radius = radius;
        _getItem = getItem;
        _item = item;
        _image = GetComponent<Image>();
        _group = GetComponent<CanvasGroup>();
        CanMove = true;
    }

    public void Play(int centerId,float targetAlpha)
    {
        if(_centerId == centerId 
        || _id == centerId 
        || (_lastCenterId == centerId && _group.alpha == targetAlpha) 
        || _item.IsCenter)
            return;

        _lastCenterId = _centerId;
        _centerId = centerId;

        if (_getItem(centerId).GetDistance(_item) <= _radius)
        {
            _group.DOKill();

            if (targetAlpha == 0)
            {
                _image.raycastTarget = false;
                CanMove = false;
            }
            else
            {
                _image.raycastTarget = true;
                CanMove = true;
            }

            _group.DOFade(targetAlpha, 0.2f).OnComplete(() =>
            {
                _lastCenterId = _centerId;
                _centerId = -1;
            });

            StartCoroutine(WaitFade(centerId, targetAlpha));
        }
    }

    private IEnumerator WaitFade(int centerId, float targetAlpha)
    {
        yield return new WaitForSeconds(0.15f);
        PlayNeighbor(centerId, targetAlpha);
    }

    public void PlayNeighbor(int centerId, float targetAlpha)
    {
        foreach (int id in _item.NeighborIds)
        {
            _getItem(id).FadeEffect.Play(centerId, targetAlpha);
        }
    }
}
