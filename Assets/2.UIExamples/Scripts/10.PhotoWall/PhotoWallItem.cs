using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhotoWallItem : MonoBehaviour, IPointerDownHandler
{
    public int ID { get; private set; }
    public bool IsCenter;
    public RectTransform Rect { get; private set; }
    private float _showScale = 5;
    public bool IsLeftBorder { get; set; }
    public bool IsRightBorder { get; set; }
    public bool IsBehindCenterPoint { get; set; }
    public List<Vector2> CenterPoints { get; set; }
    public List<int> NeighborIds { get; private set; }
    public FadeEffect FadeEffect { get; private set; }
    public Dictionary<Vector2, float> CircleXMin { get; set; }
    public Dictionary<Vector2, float> CircleXMax { get; set; }
    public float Radius { get; private set; }
    public Vector2 TargetPos { get; private set; }
    public FluctuateEffect FluctuateEffect { get; private set; }
    public bool CanMove
    {
        get
        {
            if (FadeEffect == null)
                return true;

            return FadeEffect.CanMove;
        }
    }

    private Action<int> _addCenterAction;
    private Action<int> _removeCenterAction;

    public void Init(int id, Vector2 targetPos, int column, int totalNum, Func<int, PhotoWallItem> getItem)
    {
        TargetPos = targetPos;
        CenterPoints = new List<Vector2>();
        CircleXMin = new Dictionary<Vector2, float>();
        CircleXMax = new Dictionary<Vector2, float>();
        ID = id;
        IsCenter = false;
        Rect = GetComponent<RectTransform>();
        NeighborIds = new List<int>();
        SetName(id.ToString());
        GetNeighborId(id, column, totalNum);

        FadeEffect = gameObject.AddComponent<FadeEffect>();
        Radius = GetRadius(Rect, _showScale);
        FadeEffect.Init(id, Radius, this, getItem);

        FluctuateEffect = gameObject.AddComponent<FluctuateEffect>();
        FluctuateEffect.Init(id, this, getItem);
    }

    public void ResetMoveData()
    {
        Rect.anchoredPosition = TargetPos;
        IsBehindCenterPoint = false;
        CenterPoints.Clear();
        CircleXMax.Clear();
        CircleXMin.Clear();
    }

    public void AddCenterListener(Action<int> addCenterAction)
    {
        _addCenterAction = addCenterAction;
    }

    public void RemoveCenterListener(Action<int> removeCenterAction)
    {
        _removeCenterAction = removeCenterAction;
    }

    private float GetRadius(RectTransform rect, float scaleSize)
    {
        float hypotenus = Mathf.Sqrt(Mathf.Pow(rect.rect.width * scaleSize, 2) + Mathf.Pow(rect.rect.height * scaleSize, 2));

        return hypotenus * 0.5f + 30f;
    }

    private void GetNeighborId(int id, int column, int totalNum)
    {
        int idTemp = id - 1;

        if (idTemp >= 0 && !IsLeftBorder)
            NeighborIds.Add(idTemp);

        idTemp = id + 1;
        if (idTemp < totalNum && !IsRightBorder)
            NeighborIds.Add(idTemp);

        idTemp = id - column;
        if (idTemp >= 0)
            NeighborIds.Add(idTemp);

        idTemp = id + column;
        if (idTemp < totalNum)
            NeighborIds.Add(idTemp);
    }

    private void SetName(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float maxScale = 50;
        float time = 0.1f;

        if (!IsCenter)
        {
            IsCenter = true;

            if (_addCenterAction != null)
                _addCenterAction(ID);

            Sequence quence = DOTween.Sequence();
            quence.Append(Rect.DOScale(maxScale, time));
            quence.Append(Rect.DOScale(_showScale, time));
            quence.OnComplete(() =>
            {
                FadeEffect.PlayNeighbor(ID, 0);
                FluctuateEffect.PlayNeighbor(ID);
            });
        }
        else
        {
            IsCenter = false;

            if (_removeCenterAction != null)
                _removeCenterAction(ID);

            Sequence quence = DOTween.Sequence();
            quence.Append(Rect.DOScale(maxScale, time));
            quence.Append(Rect.DOScale(1, time));
            quence.OnComplete(() =>
            {
                FadeEffect.PlayNeighbor(ID, 1);
            });
        }
    }

    public float GetDistance(PhotoWallItem item)
    {
        return Vector2.Distance(Rect.anchoredPosition, item.Rect.anchoredPosition);
    }
}
