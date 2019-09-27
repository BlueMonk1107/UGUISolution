using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorUtil 
{
    public static void MirrorVertex(Rect rect, List<UIVertex> vertices,MirrorType mirrorType)
    {
        int count = vertices.Count;

        switch (mirrorType)
        {
            case MirrorType.HORIZONTAL:
                ExtendLength(vertices, count);
                MirrorVertex(rect, vertices, count, MirrorHorizontal);
                break;
            case MirrorType.VERTICAL:
                ExtendLength(vertices, count);
                MirrorVertex(rect, vertices, count, MirrorVertical);
                break;
            case MirrorType.ALL:
                ExtendLength(vertices, count * 3);
                MirrorVertex(rect, vertices, count, MirrorHorizontal);
                MirrorVertex(rect, vertices, count * 2, MirrorVertical);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mirrorType), mirrorType, null);
        }
    }
    
    private static void MirrorVertex(Rect rect, List<UIVertex> vertices,int count,Func<Rect,Vector3,Vector3> getPos)
    {
        UIVertex vertex;
        for (int i = 0; i < count; i++)
        {
            vertex = vertices[i];
            vertex.position = getPos(rect, vertex.position);
            vertices.Add(vertex);
        }
    }

    private static Vector3 MirrorHorizontal(Rect rect, Vector3 pos)
    {
        return new Vector3(rect.center.x * 2 - pos.x,pos.y,pos.z);
    }
    
    private static Vector3 MirrorVertical(Rect rect, Vector3 pos)
    {
        return new Vector3(pos.x,rect.center.y * 2 - pos.y,pos.z);
    }

    private static void ExtendLength(List<UIVertex> vertices,int addLength)
    {
        int length = vertices.Count + addLength;
        if (vertices.Capacity < length)
        {
            vertices.Capacity = length;
        }
    }

    public static void RemoveInvalidVertex(List<UIVertex> vertices)
    {
        int count = vertices.Count;

        int i = 0;

        while (i < count)
        {
            UIVertex v1 = vertices[i];
            UIVertex v2 = vertices[i+1];
            UIVertex v3 = vertices[i+2];

            if (v1.position == v2.position
                || v2.position == v3.position
                || v3.position == v1.position)
            {
                vertices[i] = vertices[count - 3];
                vertices[i + 1] = vertices[count - 2];
                vertices[i+3] = vertices[count - 1];
                count -= 3;
            }
            else
            {
                i += 3;
            }
        }

        if (count < vertices.Count)
        {
            vertices.RemoveRange(count,vertices.Count - count);
        }
    }
}
