using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 direction = (b-a).normalized;
        float dotProduct = Mathf.Clamp(Vector3.Dot(target - a, direction), 0, Vector3.Distance(a, b));
        return a + direction * dotProduct;
    }
}
