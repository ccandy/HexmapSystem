using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{


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

    void Start()
    {
        
    }

    
}
