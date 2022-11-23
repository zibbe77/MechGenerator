using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class ProceduralMeshSkript : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    //grid settings 
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridsizes;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        MakeMeshData();
        UpdateMesh();
    }

    void MakeMeshData()
    {
        vertices = new Vector3[(gridsizes + 1) * (gridsizes + 1)];
        triangles = new int[gridsizes * gridsizes * 6];

        float vertexOffset = cellSize * 0.5f;

        int v = 0;
        int t = 0;

        //creating vertex grid 
        for (int x = 0; x <= gridsizes; x++)
        {
            for (int y = 0; y <= gridsizes; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, (x + y) * 0.2f, (y * cellSize) - vertexOffset);
                v++;
            }
        }

        //reset 
        v = 0;

        //creat truangels 
        for (int x = 0; x < gridsizes; x++)
        {
            for (int y = 0; y < gridsizes; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridsizes + 1);
                triangles[t + 5] = v + (gridsizes + 1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
