using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh _hexMesh;
    List<Vector3> _vertics;
    List<int> _triangles;

    // Start is called before the first frame update
    void Start()
    {
        _hexMesh = new Mesh();
        _vertics = new List<Vector3>();
        _triangles = new List<int>();

        MeshFilter _meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshFilter.mesh = _hexMesh;
    }


    public void Triangulate(HexCell[] cells)
    {
        _hexMesh.Clear();
        _vertics.Clear();
        _triangles.Clear();

        for (int n = 0; n < cells.Length; n++)
        {
            HexCell _cell = cells[n];
            TriangulateCell(_cell);
        }
        _hexMesh.vertices = _vertics.ToArray();
        _hexMesh.triangles = _triangles.ToArray();
        _hexMesh.RecalculateNormals();

    }

    private void TriangulateCell(HexCell cell)
    {
        Vector3 v1 = cell.transform.localPosition;
        for(int n =0; n < 6; n++)
        {
            Vector3 v2 = v1 + HexMatrics.Corners[n];
            Vector3 v3 = v1 + HexMatrics.Corners[n+1];

            AddTriangle(v1, v2, v3);
        }
            
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int indexCount = _vertics.Count;

        _vertics.Add(v1);
        _vertics.Add(v2);
        _vertics.Add(v3);

        _triangles.Add(indexCount);
        _triangles.Add(indexCount + 1);
        _triangles.Add(indexCount + 2);
    }
    
}
