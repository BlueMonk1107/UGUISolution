using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class TiledMirror : IMirror
{

    private MirrorType _mirrorType;
    private Image _image;
    public bool CanDraw
    {
        get { return _image.overrideSprite != null; }
    }

    public void Init(MirrorType mirrorType, Image image)
    {
        _mirrorType = mirrorType;
        _image = image;
    }

    public void Draw(List<UIVertex> vertices)
    {
        Sprite sprite = _image.overrideSprite;
        Vector4 uv = DataUtility.GetOuterUV(sprite);

        float width = sprite.rect.width / _image.pixelsPerUnit;
        float height = sprite.rect.height / _image.pixelsPerUnit;

        Rect rect = _image.GetPixelAdjustedRect();

        for (int i = 0; i < vertices.Count /3; i++)
        {
            UIVertex v1 = vertices[i * 3];
            UIVertex v2 = vertices[i* 3+1];
            UIVertex v3 = vertices[i* 3+2];

            Vector2 center = GetCenter(v1.position, v2.position, v3.position);

            if (_mirrorType == MirrorType.HORIZONTAL || _mirrorType == MirrorType.ALL)
            {
                int id = Mathf.CeilToInt((center.x - rect.xMin) / width);
                if (id % 2 == 0)
                {
                    v1.uv0 = GetFlipUV(v1.uv0, uv, true);
                    v2.uv0 = GetFlipUV(v2.uv0, uv, true);
                    v3.uv0 = GetFlipUV(v3.uv0, uv, true);
                }
            }
            
            if (_mirrorType == MirrorType.VERTICAL || _mirrorType == MirrorType.ALL)
            {
                int id = Mathf.CeilToInt((center.y - rect.yMin) / height);
                if (id % 2 == 0)
                {
                    v1.uv0 = GetFlipUV(v1.uv0, uv, false);
                    v2.uv0 = GetFlipUV(v2.uv0, uv, false);
                    v3.uv0 = GetFlipUV(v3.uv0, uv, false);
                }
            }

            vertices[i* 3] = v1;
            vertices[i* 3+1] = v2;
            vertices[i* 3+2] = v3;
        }
    }

    private Vector2 GetCenter(Vector3 p1,Vector3 p2,Vector3 p3)
    {
        float x = GetCenterValue(p1.x, p2.x, p3.x);
        float y = GetCenterValue(p1.y, p2.y, p3.y);
        return new Vector2(x,y);
    }

    private float GetCenterValue(float f1,float f2,float f3)
    {
        float min = Mathf.Min(Mathf.Min(f1, f2), f3);
        float max = Mathf.Max(Mathf.Max(f1, f2), f3);
        return (min + max) * 0.5f;
    }

    private Vector2 GetFlipUV(Vector2 uv,Vector4 outerUv,bool isHorizontal)
    {
        if (isHorizontal)
        {
            uv.x = outerUv.x + outerUv.z - uv.x;
        }
        else
        {
            uv.y = outerUv.y + outerUv.w - uv.y;
        }

        return uv;
    }
}
