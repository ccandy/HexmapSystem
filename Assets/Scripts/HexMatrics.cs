using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMatrics
{
    public const float OutterRad = 10f;
    public const float InnerRad = OutterRad * 0.866025f;

    public const float SoildFactor = 0.75f;
    public const float BlendFactor = 1 - SoildFactor;

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

    public static Vector3 GetFirstSoildCorner(HexDirection dir)
    {
        return Corners[(int)dir] * SoildFactor;
    }

    public static Vector3 GetSecondSoildCorner(HexDirection dir)
    {
        return Corners[(int)dir + 1] * SoildFactor;
    }

    public static Vector3 GetBridge(HexDirection dir)
    {
        Vector3 v1 = Corners[(int)dir];
        Vector3 v2 = Corners[(int)dir + 1];

        Vector3 bridge = (v1 + v2) * BlendFactor;

        return bridge;
    }


}


