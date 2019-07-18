using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FluctuateEffect : MonoBehaviour
{
    private int _id;
    private int _centerId, _lastCenterId;
    private PhotoWallItem _item;
    private Func<int, PhotoWallItem> _getItem;
    private Vector3 _defaultScale;
    public void Init(int id, PhotoWallItem item, Func<int, PhotoWallItem> getItem)
    {
        _id = id;
        _getItem = getItem;
        _item = item;
        _defaultScale = transform.localScale;
    }

    public void Play(int centerId)
    {
        if (_centerId == centerId
         || _id == centerId
         || _lastCenterId == centerId 
         || _item.IsCenter )
            return;

        _lastCenterId = _centerId;
        _centerId = centerId;

        transform.DOKill();
        transform.localScale = _defaultScale;
        transform.DOScale(1.5f, 0.15f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            _lastCenterId = _centerId;
            _centerId = -1;
        }); ;

        StartCoroutine(WaitFade(centerId));
    }

    private IEnumerator WaitFade(int centerId)
    {
        yield return new WaitForSeconds(0.1f);
        PlayNeighbor(centerId);
    }

    public void PlayNeighbor(int centerId)
    {
        foreach (int id in _item.NeighborIds)
        {
            _getItem(id).FluctuateEffect.Play(centerId);
        }
    }
}
