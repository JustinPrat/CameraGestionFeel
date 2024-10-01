using System;
using UnityEngine;

[Serializable]
public class Curve
{
    [SerializeField]
    private Vector3 m_a, m_b, m_c, m_d;
    
    #if UNITY_EDITOR
    [SerializeField, Range(2, 50)] private int m_pointsNumber;
    #endif

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
    #if UNITY_EDITOR
        Gizmos.color = color;
        for (int i = 0; i < m_pointsNumber; i++)
        {
            Gizmos.DrawLine(GetPosition(i / (float)m_pointsNumber, localToWorldMatrix), GetPosition((i + 1f) / m_pointsNumber, localToWorldMatrix));
        }
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(m_a), 0.2f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(m_b), 0.2f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(m_c), 0.2f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(m_d), 0.2f);
#endif
    }
}
