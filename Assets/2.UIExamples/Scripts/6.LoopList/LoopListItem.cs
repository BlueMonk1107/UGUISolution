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
        //这个是每次子物体超出范围的ID的偏移量
        //在快速移动的时候，一次性超出范围的子项可能不止一个，存在多个的情况
        //所以这里就是根据超过范围的个数，对ID进行偏移计算
        int offset = 0;
        if (_id < _startId)
        {
            offset = _startId - _id - 1;
            ChangeId(_endId - offset);
        }
        else if (_id > _endId)
        {
            offset = _id - _endId - 1;
            ChangeId(_startId + offset);
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
