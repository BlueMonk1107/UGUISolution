using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarChart : Image
{
    [SerializeField]
    private int _pointCount;
    [SerializeField]
    private List<RectTransform> _points;
    [SerializeField]
    private Vector2 _pointSize = new Vector2(10, 10);
    [SerializeField]
    private Sprite _pointSprite;
    [SerializeField]
    private Color _pointColor = Color.white;
    [SerializeField]
    private float[] _handlerRadio;
    [SerializeField]
    private List<RadarChartHandler> _handlers; 

    // Update is called once per frame
    void Update()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        AddVerts(vh);
        AddTriangle(vh);
    }

    private void AddVerts(VertexHelper vh)
    {
        foreach (RadarChartHandler handler in _handlers)
        {
            vh.AddVert(handler.transform.localPosition, color, Vector2.zero);
        }
    }

    private void AddVertsTemplete(VertexHelper vh)
    {
        vh.AddVert(_handlers[0].transform.localPosition, color, new Vector2(0.5f,1));
        vh.AddVert(_handlers[1].transform.localPosition, color, new Vector2(0f, 1));
        vh.AddVert(_handlers[2].transform.localPosition, color, new Vector2(0f, 0));
        vh.AddVert(_handlers[3].transform.localPosition, color, new Vector2(1f, 0));
        vh.AddVert(_handlers[4].transform.localPosition, color, new Vector2(1f, 1));
    }

    private void AddTriangle(VertexHelper vh)
    {
        for (int i = 1; i < _pointCount - 1; i++)
        {
            vh.AddTriangle(0,i+1,i);
        }
    }

    public void InitPoint()
    {
        ClearPoints();
        _points = new List<RectTransform>();
        SpawnPoint();
        SetPointPos();
    }

    private void ClearPoints()
    {
        if(_points == null)
            return;

        foreach (RectTransform point in _points)
        {
            if(point != null)
                DestroyImmediate(point);
        }
    }

    private void SpawnPoint()
    {
        for (int i = 0; i < _pointCount; i++)
        {
            GameObject point = new GameObject("Point" + i);
            point.transform.SetParent(transform);
            _points.Add(point.AddComponent<RectTransform>());
        }
    }

    private void SetPointPos()
    {
        float radian = 2 * Mathf.PI / _pointCount;
        float radius = 100;

        float curRadian = 2 * Mathf.PI / 4.0f;
        for (int i = 0; i < _pointCount; i++)
        {
            float x = Mathf.Cos(curRadian) * radius;
            float y = Mathf.Sin(curRadian) * radius;
            curRadian += radian;
            _points[i].anchoredPosition = new Vector2(x, y);
        }
    }

    public void InitHandlers()
    {
        ClearHandlers();
        _handlers = new List<RadarChartHandler>();
        SpawnHandlers();
        SetHandlerPos();
    }

    private void ClearHandlers()
    {
        if (_handlers == null)
            return;

        foreach (RadarChartHandler handler in _handlers)
        {
            if (handler != null)
                DestroyImmediate(handler.gameObject);
        }
    }

    private void SpawnHandlers()
    {
        RadarChartHandler handler = null;
        for (int i = 0; i < _pointCount; i++)
        {
            GameObject point = new GameObject("Handler" + i);
            point.AddComponent<RectTransform>();
            point.AddComponent<Image>();
            handler = point.AddComponent<RadarChartHandler>();
            handler.SetParent(transform);
            handler.ChangeSprite(_pointSprite);
            handler.ChangeColor(_pointColor);
            handler.SetSize(_pointSize);
            handler.SetScale(Vector3.one);
            _handlers.Add(handler);
        }

    }

    private void SetHandlerPos()
    {
        if (_handlerRadio == null || _handlerRadio.Length != _pointCount)
        {
            for (int i = 0; i < _pointCount; i++)
            {
                _handlers[i].SetPos(_points[i].anchoredPosition);
            }
        }
        else
        {
            for (int i = 0; i < _pointCount; i++)
            {
                _handlers[i].SetPos(_points[i].anchoredPosition * _handlerRadio[i]);
            }
        }
    }
}
