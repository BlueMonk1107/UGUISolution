using System;
using UnityEngine;
using UnityEngine.UI;

public class LoopListItem : MonoBehaviour
{
    private int _id;
    private RectTransform _rect;
    public RectTransform Rect
    {
        get
        {
            if (_rect == null)
                _rect = GetComponent<RectTransform>();
            return _rect;
        }
    }

    private Image _icon;
    public Image Icon
    {
        get
        {
            if (_icon == null)
                _icon = transform.Find("Image").GetComponent<Image>();
            return _icon;
        }
    }

    private Text _des;
    public Text Des
    {
        get
        {
            if (_des == null)
                _des = transform.Find("Text").GetComponent<Text>();
            return _des;
        }
    }

    private Func<int, LoopListItemModel> _getData;
    private RectTransform _content;
    private float _offset;
    private int _showItemNum;
    private LoopListItemModel _model;

    public void Init(int id,float offsetY,int showItemNum)
    {
        _content = transform.parent.GetComponent<RectTransform>();
        _showItemNum = showItemNum;
        _offset = offsetY;

        ChangeId(id);
    }

    public void AddGetDataListener(Func<int, LoopListItemModel> getData)
    {
        _getData = getData;
    }


    public void OnValueChange()
    {
        int startId, endId = 0;
        UpdateIdRange(out startId,out endId);
        JudgeSelfId(startId, endId);
    }

    private void UpdateIdRange(out int startId,out int endId)
    {
        startId = Mathf.FloorToInt(_content.anchoredPosition.y/(Rect.rect.height + _offset));
        endId = startId + _showItemNum - 1;
    }

    private void JudgeSelfId(int _startId, int _endId)
    {
        if (_id < _startId)
        {
            ChangeId(_endId);
        }
        else if (_id > _endId)
        {
            ChangeId(_startId);
        }
    }

    private void ChangeId(int id)
    {
        if (_id != id && JudgeIdValid(id))
        {
            _id = id;
            _model = _getData(id);
            Icon.sprite = _model.Icon;
            Des.text = _model.Describe;
            SetPos();
        }
    }

    private void SetPos()
    {
        Rect.anchoredPosition = new Vector2(0, - _id * (Rect.rect.height + _offset));
    }

    private bool JudgeIdValid(int id)
    {
        return !_getData(id).Equals(new LoopListItemModel());
    }
}
