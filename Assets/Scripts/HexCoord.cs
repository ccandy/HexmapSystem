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

    public static HexCoord FromPosition(Vector3 pos)
    {
        float x = pos.x / (HexMatrics.InnerRad * 2f);
        float y = -x;

        float offset = pos.z / (HexMatrics.OutterRad * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            Debug.LogWarning("rounding error!");

            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }

        }

        return new HexCoord(iX, iZ);

    }






}
