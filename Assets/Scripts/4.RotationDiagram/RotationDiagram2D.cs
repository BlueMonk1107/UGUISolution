using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RotationDiagram2D : MonoBehaviour
{
    public Vector2 ItemSize;
    public Sprite[] ItemSprites;
    public float Offset;
    public float ScaleTimesMin;
    public float ScaleTimesMax;

    private List<RotationDiagramItem> _items;
    private List<ItemPosData> _posData;

    // Start is called before the first frame update
    void Start()
    {
        _items = new List<RotationDiagramItem>();
        _posData = new List<ItemPosData>();
        CreateItem();
        CalulateData();
        SetItemData();
    }

    private GameObject CreateTemplate()
    {
        GameObject item = new GameObject("Template");
        item.AddComponent<RectTransform>().sizeDelta = ItemSize;
        item.AddComponent<Image>();
        item.AddComponent<RotationDiagramItem>();
        return item;
    }

    private void CreateItem()
    {
        GameObject template = CreateTemplate();
        RotationDiagramItem itemTemp = null;
        foreach (Sprite sprite in ItemSprites)
        {
            itemTemp = Instantiate(template).GetComponent<RotationDiagramItem>();
            itemTemp.SetParent(transform);
            itemTemp.SetSprite(sprite);
            itemTemp.AddMoveListener(Change);
            _items.Add(itemTemp);
        }

        Destroy(template);
    }

    private void Change(float offsetX)
    {
        int symbol = offsetX > 0 ? 1 : -1;
        Change(symbol);
    }

    private void Change(int symbol)
    {
        foreach (RotationDiagramItem item in _items)
        {
            item.ChangeId(symbol, _items.Count);
        }

        for (int i = 0; i < _posData.Count; i++)
        {
            _items[i].SetPosData(_posData[_items[i].PosId]);
        }
    }

    private void CalulateData()
    {
        List<ItemData> itemDatas = new List<ItemData>();

        float length = (ItemSize.x + Offset)*_items.Count;
        float radioOffset = 1/(float) _items.Count;

        float radio = 0;
        for (int i = 0; i < _items.Count; i++)
        {
            ItemData itemData = new ItemData();
            itemData.PosId = i;
            itemDatas.Add(itemData);

            _items[i].PosId = i;

            ItemPosData data = new ItemPosData();
            data.X = GetX(radio, length);
            data.ScaleTimes = GetScaleTimes(radio, ScaleTimesMax, ScaleTimesMin);

            radio += radioOffset;
            _posData.Add(data);
        }

        itemDatas = itemDatas.OrderBy(u => _posData[u.PosId].ScaleTimes).ToList();

        for (int i = 0; i < itemDatas.Count; i++)
        {
            _posData[itemDatas[i].PosId].Order = i;
        }
    }

    private void SetItemData()
    {
        for (int i = 0; i < _posData.Count; i++)
        {
            _items[i].SetPosData(_posData[i]);
        }
    }

    private float GetX(float radio, float length)
    {
        if (radio > 1 || radio < 0)
        {
            Debug.LogError("当前比例必须是0-1的值");
            return 0;
        }

        if (radio >= 0 && radio < 0.25f)
        {
            return length * radio;
        }
        else if (radio >= 0.25f && radio < 0.75f)
        {
            return length * (0.5f - radio);
        }
        else
        {
            return length * (radio - 1);
        }
    }

    public float GetScaleTimes(float radio, float max, float min)
    {
        if (radio > 1 || radio < 0)
        {
            Debug.LogError("当前比例必须是0-1的值");
            return 0;
        }

        float scaleOffset = (max - min) / 0.5f;

        if (radio < 0.5f)
        {
            return max - scaleOffset * radio;
        }
        else
        {
            return max - scaleOffset * (1 - radio);
        }
    }
}

public class ItemPosData
{
    public float X;
    public float ScaleTimes;
    public int Order;
}

public struct ItemData
{
    public int PosId;
}
