using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Surface : MonoBehaviour
{

    [SerializeField]
    private int xSize, ySize;
    [SerializeField]
    private Material material;
    [SerializeField]
    private float wait;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Awake()
    {
        vertices = new Vector3[(xSize + 1)*(ySize + 1)];
        triangles = new int[xSize * ySize * 6];

        GetComponent<MeshRenderer>().material = material;

        StartCoroutine(generate());
    }

    private IEnumerator generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return new WaitForSeconds(wait);
            }
        }
        mesh.vertices = vertices;

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        for(int i = 0; i < vertices.Length; i++)
        {
            Color color = new Color(vertices[i].x / xSize, vertices[i].y / ySize, vertices[i].z / 1);
            Gizmos.color = color;
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

    }
}
