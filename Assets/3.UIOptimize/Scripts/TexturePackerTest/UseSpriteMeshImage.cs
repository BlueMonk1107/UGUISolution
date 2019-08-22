using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

public class UseSpriteMeshImage : Image
{
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if(sprite == null)
            return;
        
        base.OnPopulateMesh(toFill);
        
        List<UIVertex> vertices = new List<UIVertex>();
        toFill.GetUIVertexStream(vertices);
        Vector2 leftBottom = vertices[0].position;
        Vector2 rightTop = vertices[2].position;
        Vector2 imageOffset = new Vector2(rightTop.x - leftBottom.x,rightTop.y - leftBottom.y);
        
        Vector2 offset = new Vector2(imageOffset.x / sprite.bounds.size.x,imageOffset.y / sprite.bounds.size.y);

        vertices.Clear();
        UIVertex vertex;
        for (int i = 0; i < sprite.vertices.Length; i++)
        {
            vertex = new UIVertex();
            vertex.position = new Vector3(sprite.vertices[i].x * offset.x,sprite.vertices[i].y * offset.y);
            vertex.color = color;
            vertex.uv0 = sprite.uv[i];
            vertices.Add(vertex);
        }

        List<int> triangles = new List<int>();
        for (int i = 0; i < sprite.triangles.Length; i++)
        {
            triangles.Add(sprite.triangles[i]);
        }
        
        toFill.Clear();
        toFill.AddUIVertexStream(vertices,triangles);
    }
}
