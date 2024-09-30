using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 direction = (b-a).normalized;
        float dotProduct = Mathf.Clamp(Vector3.Dot(target - a, direction), 0, Vector3.Distance(a, b));
        return a + direction * dotProduct;
    }

    public static Vector3 LinearBezier(Vector3 a, Vector3 b, float t)
    {
        t = Mathf.Clamp01(t);
        return (1 - t) * a + t * b;
    }

    public static Vector3 QuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        t = Mathf.Clamp01(t);
        return (1 - t) * LinearBezier(a, b, t) + t * LinearBezier(b, c, t);
    }
    
    public static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        t = Mathf.Clamp01(t);
        return (1 - t) * QuadraticBezier(a, b, c, t) + t * QuadraticBezier(b, c, d, t);
    }
}
