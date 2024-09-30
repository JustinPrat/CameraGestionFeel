using System;
using UnityEngine;

[Serializable]
public class Curve
{
    [SerializeField]
    private Vector3 m_a, m_b, m_c, m_d;

    [SerializeField, Range(0.2f, 1f)] private float m_pointInterval;

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(m_a, m_b, m_c, m_d, t);
    }
    
    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return MathUtils.CubicBezier(localToWorldMatrix.MultiplyPoint(m_a), localToWorldMatrix.MultiplyPoint(m_b),
            localToWorldMatrix.MultiplyPoint(m_c), localToWorldMatrix.MultiplyPoint(m_d), t);
    }

    public void DrawGizmos(Color color, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = color;
        for (float i = 0f; i < 1f; i += m_pointInterval)
        {
            Gizmos.DrawLine(GetPosition(i), GetPosition(i + m_pointInterval));
        }
    }
}
