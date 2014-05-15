using UnityEngine;
using System.Collections;

public class Util
{

    public static float GetDistance(Vector3 v1, Vector3 v2) // berechnet horizontalen abstand zwischen zwei vektoren
    {
        float a = Mathf.Abs(v1.x - v2.x);
        float b = Mathf.Abs(v1.z - v2.z);

        return Mathf.Sqrt(a * a + b * b);
    }

    public static void ChangeAlpha(Material mat, float delta)
    {
        Color color = mat.color;
        color.a = Mathf.Max(Mathf.Min(color.a + delta, 0.0f), 1.0f);
        mat.color = color;
    }
}
