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
        Vector3 center = cell.transform.localPosition;
        for(HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            Vector3 v1 = center + HexMatrics.GetFirstSoildCorner(d);
            Vector3 v2 = center + HexMatrics.GetSecondSoildCorner(d);
            AddTriangle(center, v1, v2);
            AddTriangleColor(cell.CellColor);

            Vector3 v3 = center + HexMatrics.GetFirstCorner(d);
            Vector3 v4 = center + HexMatrics.GetSecondCorner(d);

            AddQuad(v1, v2, v3, v4);




            HexCell prevCell = cell.GetNeiborCell(d.Prev()) ?? cell;
            HexCell neiborCell = cell.GetNeiborCell(d) ?? cell;
            HexCell nextCell = cell.GetNeiborCell(d.Next()) ?? cell;

            Color edgeColor1 = (cell.CellColor + neiborCell.CellColor + prevCell.CellColor) /3;
            Color edgeColor2 = (cell.CellColor + neiborCell.CellColor + nextCell.CellColor) / 3;

            AddQuadColor(cell.CellColor, cell.CellColor, edgeColor1, edgeColor2);
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

    private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        int indexCount = _vertics.Count;

        _vertics.Add(v1);
        _vertics.Add(v2);
        _vertics.Add(v3);
        _vertics.Add(v4);

        _triangles.Add(indexCount);
        _triangles.Add(indexCount + 2);
        _triangles.Add(indexCount + 1);
        _triangles.Add(indexCount + 1);
        _triangles.Add(indexCount + 2);
        _triangles.Add(indexCount + 3);

    }
    private void AddTriangleColor(Color color)
    {
        _colors.Add(color);
        _colors.Add(color);
        _colors.Add(color);
    }

    private void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        _colors.Add(c1);
        _colors.Add(c2);
        _colors.Add(c3);
    }

    private void AddQuadColor(Color c1, Color c2, Color c3, Color c4)
    {
        _colors.Add(c1);
        _colors.Add(c2);
        _colors.Add(c3);
        _colors.Add(c4);
    }

}
