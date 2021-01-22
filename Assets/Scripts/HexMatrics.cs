using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMatrics
{
    public const float OutterRad = 10f;
    public const float InnerRad = OutterRad * 0.866025f;

    public static Vector3[] Corners =
    {
        new Vector3(0,0, OutterRad),
        new Vector3(InnerRad, 0, 0.5f * OutterRad),
        new Vector3(InnerRad, 0, -0.5f * OutterRad),
        new Vector3(0,0,-OutterRad),
        new Vector3(-InnerRad, 0, -0.5f * OutterRad),
        new Vector3(-InnerRad, 0, 0.5f * OutterRad),
        new Vector3(0,0, OutterRad)
    };

    public static Vector3 GetFirstCorner(HexDirection dir)
    {
        return Corners[(int)dir];
    }

    public static Vector3 GetSecondCorner(HexDirection dir)
    {
        return Corners[(int)dir + 1];
    }
}


