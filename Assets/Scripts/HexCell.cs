using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{

    [SerializeField]
    HexCell[] NeiborCells;

    private int _elevation;
    public int Elevation
    {
        set
        {
            _elevation = value;
            Vector3 pos = transform.localPosition;
            pos.y = _elevation * HexMatrics.ElevationStep;
            transform.localPosition = pos;
        }
        get
        {
            return _elevation;
        }
    }


    private Color _cellColor;
    public Color CellColor
    {
        set
        {
            _cellColor = value;
        }
        get
        {
            return _cellColor;
        }
    }
    private HexCoord _hexCoord;
    public HexCoord HexCoord
    {
        set
        {
            _hexCoord = value;
        }
        get
        {
            return _hexCoord;
        }
    }
    
    public HexCell GetNeiborCell(HexDirection dir)
    {
        int d = (int)dir;
        return NeiborCells[d];
    }

    public void SetNeiborCell(HexCell c, HexDirection dir)
    {
        int d = (int)dir;
        NeiborCells[d] = c;

        HexDirection oppsiteDir = dir.Oppsite();
        c.NeiborCells[(int)oppsiteDir] = this;
    }

}
