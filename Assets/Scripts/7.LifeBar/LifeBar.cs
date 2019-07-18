using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;
    private List<LifeBarData> _data;
    private LifeBarItem _nextBar;
    private LifeBarItem _currentBar;
    private float _unitLifeScale;
    private int _currentIndex;

    public void Init(Transform target, int lifeMax, List<LifeBarData> data)
    {
        _currentIndex = 0;
        _target = target;
        _offset = GetOffset(target);
        _data = data;
        _nextBar = transform.Find("NextBar").gameObject.AddComponent<LifeBarItem>();
        _currentBar = transform.Find("CurrentBar").gameObject.AddComponent<LifeBarItem>();
        _nextBar.Init();
        _currentBar.Init();

        RectTransform rect = GetComponent<RectTransform>();
        _unitLifeScale = rect.rect.width * data.Count / lifeMax;

        SetBarData(_currentIndex,data);
    }

    private Vector3 GetOffset(Transform target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null)
            return Vector3.zero;
        return Vector3.up * renderer.bounds.max.y;
    }

    public void Update()
    {
        if (_target == null)
            return;

        transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
    }

    public void ChangeLife(float value)
    {
        float width = _currentBar.ChangeLife(value * _unitLifeScale);

        if (width < 0 && ChangeIndex(1))
        {
            Exchange();
            _currentBar.transform.SetAsLastSibling();
            _nextBar.ResetToWidth();
            SetBarData(_currentIndex,_data);
            ChangeLife(width / _unitLifeScale);
        }
        else if (width > 0 && ChangeIndex(-1))
        {
            Exchange();
            _currentBar.transform.SetAsLastSibling();
            _currentBar.ResetToZero();
            SetBarData(_currentIndex, _data);
            ChangeLife(width/ _unitLifeScale);
        }
    }

    // -1 代表加血 1代表减血
    private bool ChangeIndex(int symbol)
    {
        int index = _currentIndex + symbol;
        if (index >= 0 && index < _data.Count)
        {
            _currentIndex = index;
            return true;
        }

        return false;
    }

    private void Exchange()
    {
        var temp = _nextBar;
        _nextBar = _currentBar;
        _currentBar = temp;
    }

    private void SetBarData(int index, List<LifeBarData> data)
    {
        if (index < 0 || index >= data.Count)
            return;

        _currentBar.SetData(data[index]);

        if (index + 1 >= data.Count)
        {
            _nextBar.SetData(new LifeBarData(null,Color.white));
        }
        else
        {
            _nextBar.SetData(data[index + 1]);
        }
    }
}

public struct LifeBarData
{
    public Sprite BarSprite;
    public Color BarMainColor;

    public LifeBarData(Sprite sprite, Color mainColor)
    {
        BarSprite = sprite;
        BarMainColor = mainColor;
    }
}
