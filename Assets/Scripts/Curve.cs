using System;
using UnityEngine;

[Serializable]
public class Curve
{
    [field:SerializeField] public Vector3 A { get; set; }
    [field:SerializeField] public Vector3 B { get; set; }
    [field:SerializeField] public Vector3 C { get; set; }
    [field:SerializeField] public Vector3 D { get; set; }
    
    #if UNITY_EDITOR
    [SerializeField, Range(2, 50)] private int m_pointsNumber;
    #endif

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }
    
    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return MathUtils.CubicBezier(localToWorldMatrix.MultiplyPoint(A), localToWorldMatrix.MultiplyPoint(B),
            localToWorldMatrix.MultiplyPoint(C), localToWorldMatrix.MultiplyPoint(D), t);
    }

    public void DrawGizmos(Color color, Matrix4x4 localToWorldMatrix)
    {
    #if UNITY_EDITOR
        Gizmos.color = color;
        for (int i = 0; i < m_pointsNumber; i++)
        {
            Gizmos.DrawLine(GetPosition(i / (float)m_pointsNumber, localToWorldMatrix), GetPosition((i + 1f) / m_pointsNumber, localToWorldMatrix));
        }
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), 0.1f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), 0.1f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), 0.1f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), 0.1f);
    #endif
    }
}