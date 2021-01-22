using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    private Mesh _hexMesh;
    private List<Vector3> _vertics;
    private List<int> _triangles;
    private MeshCollider _hexCollider;

    private List<Color> _colors;

    // Start is called before the first frame update
    void Start()
    {
        _hexMesh = new Mesh();
        _vertics = new List<Vector3>();
        _triangles = new List<int>();
        _colors = new List<Color>();

        MeshFilter _meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshFilter.mesh = _hexMesh;

        _hexCollider = gameObject.AddComponent<MeshCollider>();

    }


    public void Triangulate(HexCell[] cells)
    {
        _hexMesh.Clear();
        _vertics.Clear();
        _triangles.Clear();
        _colors.Clear();

        for (int n = 0; n < cells.Length; n++)
        {
            HexCell _cell = cells[n];
            TriangulateCell(_cell);
        }
        _hexMesh.vertices = _vertics.ToArray();
        _hexMesh.triangles = _triangles.ToArray();
        _hexMesh.colors = _colors.ToArray();

        _hexMesh.RecalculateNormals();
        _hexCollider.sharedMesh = _hexMesh;

    }

    private void TriangulateCell(HexCell cell)
    {
        Vector3 v1 = cell.transform.localPosition;
        for(HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            Vector3 v2 = v1 + HexMatrics.GetFirstCorner(d);
            Vector3 v3 = v1 + HexMatrics.GetSecondCorner(d);

            AddTriangle(v1, v2, v3);
            AddTriangleColor(cell.CellColor);
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

    

    private void AddTriangleColor(Color color)
    {
        _colors.Add(color);
        _colors.Add(color);
        _colors.Add(color);
    }



}
