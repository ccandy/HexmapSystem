using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoord
{
    [SerializeField]
    private int _x, _z;

    private int _y;
    public int X
    {
        set
        {
            _x = value;
        }
        get
        {
            return _x;
        }
    }

    public int Z
    {
        set
        {
            _z = value;
        }
        get
        {
            return _z;
        }
    }

    public int Y
    {
        get
        {
            return _y;
        }
    }

    public HexCoord(int x, int z)
    {
        _x = x;
        _z = z;
        _y = -_x - _z;
    }

    public override string ToString()
    {
        return "X: " + X.ToString() + ", Y:" + Y.ToString() + ", Z: " + Z.ToString();
    }

    public string ToStringOnSeparateLines()
    {
       return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();

    }

    public static HexCoord FromOffsetCoord(int x, int z)
    {
        return new HexCoord(x - z/2, z);
    }





}
