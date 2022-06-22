using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugLine
{
    public static void DrawWireSphere(Vector3 centor, float radius = 1.0f, Color color = new Color(), int splitNum = 16)
    {
        DrawCircleXY(centor, radius, color, splitNum);
        DrawCircleYZ(centor, radius, color, splitNum);
        DrawCircleXZ(centor, radius, color, splitNum);
    }

    public static void DrawCircleXY(Vector3 centor, float radius = 1.0f, Color color = new Color(), int splitNum = 16)
    {
        for (int i = 0; i < splitNum; i++)
        {
            float angleStart = 2 * Mathf.PI / splitNum * i;
            float angleEnd = 2 * Mathf.PI / splitNum * (i + 1);
            Vector3 start = centor + new Vector3(Mathf.Cos(angleStart), Mathf.Sin(angleStart), 0) * radius;
            Vector3 end = centor + new Vector3(Mathf.Cos(angleEnd), Mathf.Sin(angleEnd), 0) * radius;
            Debug.DrawLine(start, end, color);
        }
    }

    public static void DrawCircleYZ(Vector3 centor, float radius = 1.0f, Color color = new Color(), int splitNum = 16)
    {
        for (int i = 0; i < splitNum; i++)
        {
            float angleStart = 2 * Mathf.PI / splitNum * i;
            float angleEnd = 2 * Mathf.PI / splitNum * (i + 1);
            Vector3 start = centor + new Vector3(0, Mathf.Sin(angleStart), Mathf.Cos(angleStart)) * radius;
            Vector3 end = centor + new Vector3(0, Mathf.Sin(angleEnd), Mathf.Cos(angleEnd)) * radius;
            Debug.DrawLine(start, end, color);
        }
    }

    public static void DrawCircleXZ(Vector3 centor, float radius = 1.0f, Color color = new Color(), int splitNum = 16)
    {
        for (int i = 0; i < splitNum; i++)
        {
            float angleStart = 2 * Mathf.PI / splitNum * i;
            float angleEnd = 2 * Mathf.PI / splitNum * (i + 1);
            Vector3 start = centor + new Vector3(Mathf.Cos(angleStart), 0, Mathf.Sin(angleStart)) * radius;
            Vector3 end = centor + new Vector3(Mathf.Cos(angleEnd), 0, Mathf.Sin(angleEnd)) * radius;
            Debug.DrawLine(start, end, color);
        }
    }
}
