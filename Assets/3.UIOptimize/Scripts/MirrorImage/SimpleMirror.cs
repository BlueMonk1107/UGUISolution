using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMirror : IMirror
{
    private MirrorType _mirrorType;
    private Image _image;
    public bool CanDraw
    {
        get { return true; }
    }

    public void Init(MirrorType mirrorType, Image image)
    {
        _mirrorType = mirrorType;
        _image = image;
    }

    public void Draw(List<UIVertex> vertices)
    {
        Rect rect = _image.GetPixelAdjustedRect();
        ChangeVertexPos(rect, vertices);
        Debug.Log(vertices.Count);
        MirrorUtil.RemoveInvalidVertex(vertices);
        Debug.Log(vertices.Count);
        MirrorUtil.MirrorVertex(rect,vertices,_mirrorType);
    }

    private void ChangeVertexPos(Rect rect, List<UIVertex> vertices)
    {
        Vector3 pos = Vector3.zero;
        UIVertex uiVertex;
        for (int i = 0; i < vertices.Count; i++)
        {
            uiVertex = vertices[i];
            pos = uiVertex.position;
            if (_mirrorType == MirrorType.HORIZONTAL || _mirrorType == MirrorType.ALL)
            {
                pos.x = (pos.x + rect.x) * 0.5f;
            }

            if (_mirrorType == MirrorType.VERTICAL || _mirrorType == MirrorType.ALL)
            {
                pos.y = (pos.y + rect.y + rect.height) * 0.5f;
            }

            uiVertex.position = pos;
            vertices[i] = uiVertex;
        }
    }
}
