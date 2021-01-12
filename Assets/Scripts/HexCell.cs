using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{

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
