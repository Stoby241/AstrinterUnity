using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMath
{
    public static Vector2 XYtoIJ(Vector2 xy)
    {
        float j = xy.y * 2 / (float)Math.Sqrt(3);
        float i = xy.x - j / 2;
        return new Vector2(i, j);
    }

    public static Vector2 IJtoXY(Vector2 ij)
    {
        float x = ij.x + ij.y / 2;
        float y = ij.y / 2 * (float)Math.Sqrt(3);
        return new Vector2(x, y);
    }

    public static Vector2Int roundVector(Vector2 vector)
    {
        int x = (int)vector.x;
        int y = (int)vector.y;
        return new Vector2Int(x, y);
    }

    public static void test()
    {
        int a = 0;
        switch (a)
        {
            case 1:
                    break;
        }
    }
}
