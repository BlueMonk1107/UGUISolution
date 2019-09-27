using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IMirror
{
    bool CanDraw { get; }
    void Init(MirrorType mirrorType, Image image);
    void Draw(List<UIVertex> vertices);
}

[RequireComponent(typeof(Image))]
public class MirrorImage : BaseMeshEffect
{
    public MirrorType _mirrorType = MirrorType.HORIZONTAL;
    private List<UIVertex> _vertices = new List<UIVertex>();
    
    private Dictionary<Image.Type, IMirror> _mirrors;

    private Dictionary<Image.Type, IMirror> Mirrors
    {
        get
        {
            if (_mirrors == null)
            {
                InitMirror();
            }

            return _mirrors;
        }
    }

    private void InitMirror()
    {
        _mirrors = new Dictionary<Image.Type, IMirror>();
        _mirrors.Add(Image.Type.Simple,new SimpleMirror());
        _mirrors.Add(Image.Type.Sliced,new SimpleMirror());
        _mirrors.Add(Image.Type.Tiled,new TiledMirror());
    }
    
    public override void ModifyMesh(VertexHelper vh)
    {
        if(!IsActive())
            return;
        IMirror mirror = GetMirrorObject();
        vh.GetUIVertexStream(_vertices);
        mirror.Draw(_vertices);
        vh.Clear();
        vh.AddUIVertexTriangleStream(_vertices);
    }

    private IMirror GetMirrorObject()
    {
        Image image = graphic as Image;
        IMirror mirror = Mirrors[Image.Type.Simple];
        if (Mirrors.ContainsKey(image.type))
        {
            mirror = Mirrors[image.type];
            mirror.Init(_mirrorType,image);
            if (mirror.CanDraw)
            {
                return mirror;
            }
        }
        
        mirror.Init(_mirrorType,image);
        return mirror;
    }

    public void SetNativeSize()
    {
        Image image = graphic as Image;
        if (image.overrideSprite != null)
        {
            float width = image.overrideSprite.rect.width / image.pixelsPerUnit;
            float height = image.overrideSprite.rect.height / image.pixelsPerUnit;
            image.rectTransform.anchorMax = image.rectTransform.anchorMin;
            Vector2 temp = Vector2.zero;
            switch (_mirrorType)
            {
                case MirrorType.HORIZONTAL:
                    temp.x = width * 2;
                    temp.y = height;
                    break;
                case MirrorType.VERTICAL:
                    temp.x = width;
                    temp.y = height * 2;
                    break;
                case MirrorType.ALL:
                    temp.x = width * 2;
                    temp.y = height * 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            image.rectTransform.sizeDelta = temp;
            image.SetVerticesDirty();
        }
    }

    public void SetVerticesDirty()
    {
        Image image = graphic as Image;
        image.SetVerticesDirty();
    }
}

public enum MirrorType
{
    HORIZONTAL,
    VERTICAL,
    ALL
}
