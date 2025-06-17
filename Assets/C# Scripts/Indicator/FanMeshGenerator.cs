using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

[RequireComponent(typeof(MeshFilter))]
public class FanMeshGenerator : MonoBehaviour
{
    public void Generate(float radius, float angle, int segments = 30)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        float rad = Mathf.Deg2Rad * angle;
        float step = rad / segments;

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float theta = -rad / 2 + step * i;
            vertices[i + 1] = new Vector3(Mathf.Sin(theta), 0f, Mathf.Cos(theta)) * radius;

            if (i < segments)
            {
                triangles[i * 3 + 0] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
