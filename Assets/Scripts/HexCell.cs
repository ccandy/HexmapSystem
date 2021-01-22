using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{

    [SerializeField]
    HexCell[] NeiborCells;


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
