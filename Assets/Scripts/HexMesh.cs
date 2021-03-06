﻿using System.Collections;
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

        HexCell neiborCell = cell.GetNeiborCell(dir) ?? cell;
        HexCell nextNeighbor = cell.GetNeiborCell(dir.Next())??cell ;

        //Debug.Log(neiborCell.Elevation);

        v3.y = v4.y = neiborCell.Elevation * HexMatrics.ElevationStep;

        /*AddQuad(v1, v2, v3, v4);
        AddQuadColor(cell.CellColor, neiborCell.CellColor);
        */

        TriangulateEdgeTerrace(cell, v1, v2, neiborCell, v3, v4);

        if (dir <= HexDirection.E && nextNeighbor != null)
        {
            Vector3 v5 = v2 + HexMatrics.GetBridge(dir.Next());
            v5.y = nextNeighbor.Elevation * HexMatrics.ElevationStep;
            AddTriangle(v2, v4, v5);
            AddTriangleColor(cell.CellColor, neiborCell.CellColor, nextNeighbor.CellColor);
        }

    }

    private void TriangulateEdgeTerrace(
        HexCell c1, Vector3 beginLeft, Vector3 beginRight,
        HexCell c2, Vector3 endLeft, Vector3 endRight)
    {

        Vector3 v3 = HexMatrics.TerraceLerp(beginLeft, endLeft, 1);
        Vector3 v4 = HexMatrics.TerraceLerp(beginRight, endRight, 1);
        Color color2 = HexMatrics.TerraceLerpColor(c1.CellColor, c2.CellColor, 1);


        AddQuad(beginLeft, beginRight, v3, v4);
        AddQuadColor(c1.CellColor, color2);

        for(int n = 2; n < HexMatrics.TerracesSteps; n++)
        {
            Vector3 v1 = v3;
            Vector3 v2 = v4;
            Color color1 = color2;

            v3 = HexMatrics.TerraceLerp(beginLeft, endLeft, n);
            v4 = HexMatrics.TerraceLerp(beginRight, endRight, n);
            color2 = HexMatrics.TerraceLerpColor(c1.CellColor, c2.CellColor, n);

            AddQuad(v1, v2, v3, v4);
            AddQuadColor(color1, color2);
        }



        AddQuad(v3, v4, endLeft, endRight);
        AddQuadColor(color2, c2.CellColor);

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
