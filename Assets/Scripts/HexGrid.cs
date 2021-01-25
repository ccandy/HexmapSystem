using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexGrid : MonoBehaviour
{

    private int _height = 6;
    private int _width = 6;

    private HexCell[] _cells;

    private Canvas _hexCanvas;
    private HexMesh _hexMesh;

    public HexCell HexCellPref;
    public Text HexCellTex;

    public Color DefaultColor = Color.white;
    public Color TouchColor = Color.red;

    void Awake()
    {
        if (HexCellPref == null)
        {
            Debug.LogError("HexCell Pref is null");
        }

        _cells = new HexCell[_width * _height];
        _hexCanvas = gameObject.GetComponentInChildren<Canvas>();
        if (_hexCanvas == null)
        {
            Debug.LogError("Canvas is null");
        }

        _hexMesh = gameObject.GetComponentInChildren<HexMesh>();

    }

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        for (int z = 0; z < _width; z++)
        {
            for (int x = 0; x < _height; x++)
            {
                CreateCell(x, z, i++);
            }
        }

        _hexMesh.Triangulate(_cells);
    }

    private void CreateCell(int x, int z, int i)
    {
        Vector3 pos = Vector3.zero;
        pos.x = (x + z * 0.5f - z / 2) * HexMatrics.InnerRad * 2;
        pos.y = 0;
        pos.z = z * HexMatrics.OutterRad * 1.5f;    


        HexCell _cell = MakeCell(x, z, i, pos);
        ConnectCells(_cell, x, z, i);
        MakeText(_cell, pos);

    }


    private HexCell MakeCell(int x, int z, int i, Vector3 pos)
    {
        HexCell _cell = Instantiate<HexCell>(HexCellPref);
        _cells[i] = _cell;
        _cell.transform.SetParent(transform);
        _cell.transform.localPosition = pos;
        _cell.CellColor = DefaultColor;
        _cell.HexCoord = HexCoord.FromOffsetCoord(x, z);
        _cell.name = "cell" + _cell.HexCoord.X + "," + _cell.HexCoord.Z;
        _cell.Elevation = Random.Range(1, 5);
        return _cell;
    }

    private Text MakeText(HexCell c, Vector3 pos) 
    {
        Text _tex = Instantiate<Text>(HexCellTex);
        _tex.rectTransform.SetParent(_hexCanvas.transform, false);
        _tex.rectTransform.anchoredPosition = new Vector2(pos.x, pos.z);
        _tex.text = c.HexCoord.ToString();

        return _tex;
    }

    private void ConnectCells(HexCell c, int x, int z, int i)
    {
        if (x > 0)
        {
            c.SetNeiborCell(_cells[i - 1], HexDirection.W);
        }

        if(z > 0)
        {
            if((z & 1) == 0)
            {
                c.SetNeiborCell(_cells[i - _width], HexDirection.SE);
                if(x > 0)
                {
                    c.SetNeiborCell(_cells[i - _width - 1], HexDirection.SW);
                }
            }
            else
            {
                c.SetNeiborCell(_cells[i - _width], HexDirection.SW);
                if (x < _width - 1)
                {
                    c.SetNeiborCell(_cells[i - _width + 1], HexDirection.SE);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    private void TouchCell(Vector3 pos)
    {
        HexCell cell = GetCell(pos);
        cell.CellColor = TouchColor;
        Refresh();

        //Debug.Log("touched at " + coord.X + "," + coord.Y);
    }

    public HexCell GetCell(Vector3 pos)
    {
        pos = transform.InverseTransformPoint(pos);
        HexCoord coord = HexCoord.FromPosition(pos);

        int index = coord.X + coord.Z * _width + coord.Z / 2;
        HexCell cell = _cells[index];

        return cell;
    }


    public void ColorCell(Vector3 pos, Color color)
    {
        HexCell cell = GetCell(pos);
        cell.CellColor = color;
        _hexMesh.Triangulate(_cells);
    }

    public void Refresh()
    {
        _hexMesh.Triangulate(_cells);
    }

}
