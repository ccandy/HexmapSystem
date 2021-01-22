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


            if(d <= HexDirection.SE)
            {
                TriangulateConnection(v1, v2, d, cell);
            }

            
            

        }
    }

    private void TriangulateConnection(Vector3 v1, Vector3 v2, HexDirection dir, HexCell cell)
    {

        if(cell.GetNeiborCell(dir) == null)
        {
            return;
        }

        Vector3 birdge = HexMatrics.GetBridge(dir);

        Vector3 v3 = v1 + birdge;
        Vector3 v4 = v2 + birdge;
        AddQuad(v1, v2, v3, v4);

        HexCell neiborCell = cell.GetNeiborCell(dir) ?? cell;
        AddQuadColor(cell.CellColor, neiborCell.CellColor);

        HexCell nextNeighbor = cell.GetNeiborCell(dir.Next());
        if(dir <= HexDirection.E && nextNeighbor != null)
        {
            AddTriangle(v2, v4, v2 + HexMatrics.GetBridge(dir.Next()));
            AddTriangleColor(cell.CellColor, neiborCell.CellColor, nextNeighbor.CellColor);
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

    private void AddQuadColor(Color c1, Color c2)
    {
        _colors.Add(c1);
        _colors.Add(c1);

        _colors.Add(c2);
        _colors.Add(c2);

    }


}
