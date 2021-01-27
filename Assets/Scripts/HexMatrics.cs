using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMatrics
{
    public const float OutterRad = 10f;
    public const float InnerRad = OutterRad * 0.866025f;

    public const float SoildFactor = 0.75f;
    public const float BlendFactor = 1 - SoildFactor;

    public const float ElevationStep = 5f;

    public const float TerracesPerSlop = 2;
    public const float TerracesSteps = TerracesPerSlop * 2 + 1;

    public const float HorizontalTerraceStepSize = 1 / TerracesSteps;
    public const float VerticalTerraceStepSize = 1 / (TerracesPerSlop + 1);

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

    public static Vector3 TerraceLerp(Vector3 a, Vector3 b, int step)
    {
        float h = step * HexMatrics.HorizontalTerraceStepSize;
        a.x += (b.x - a.x) * h;
        a.z += (b.z - a.z) * h;

        //float v = step * HexMatrics.VerticalTerraceStepSize;
        float v = ((step + 1) / 2) * HexMatrics.VerticalTerraceStepSize;
        a.y += (b.y - a.y) * v;
        return a;
        
    }

    public static Color TerraceLerpColor(Color c1, Color c2, int step)
    {
        float h = step * HexMatrics.HorizontalTerraceStepSize;
        return Color.Lerp(c1, c2, h);
    }

    public static HexEdgeType GetEdgeType(int e1, int e2)
    {
        if(e1 == e2)
        {
            return HexEdgeType.Flat;
        }

        int delta = e1 - e2;
        if(delta == -1 || delta == 1)
        {
            return HexEdgeType.Slope;
        }
        return HexEdgeType.Cliff;

    }

    


}


