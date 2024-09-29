using System;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    [Header("Rail Settings")]
    [SerializeField] private bool m_isLoop;
    [SerializeField] private List<Transform> m_railPoints;

    public float RailLength { get; private set; }

    private void Start()
    {
        RailLength = 0f;
        if (m_railPoints != null)
        {
            int railCount = m_railPoints.Count;
            for (int i = 0; i < railCount - (m_isLoop ? 0 : 1); i++)
            {
                RailLength += Vector3.Distance(m_railPoints[i].position, m_railPoints[(i + 1) % railCount].position);
            }
        }
    }

    public Vector3 GetPosition(float distance)
    {
        distance = m_isLoop ? Mathf.Repeat(distance, RailLength) : Mathf.Clamp(distance, 0, RailLength);
        Debug.Log(distance);
        int railCount = m_railPoints.Count;
        for (int i = 0; i < railCount - (m_isLoop ? 0 : 1); i++)
        {
            float pointsDistance = Vector3.Distance(m_railPoints[i].position, m_railPoints[(i + 1) % railCount].position);
            if (pointsDistance < distance)
            {
                distance -= pointsDistance;
            }
            else
            {
                return Vector3.Lerp(m_railPoints[i].position, m_railPoints[(i + 1) % railCount].position, distance / pointsDistance);
            }
        }
        return m_railPoints[railCount - 1].position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (m_railPoints != null)
        {
            int railCount = m_railPoints.Count;
            for (int i = 0; i < railCount - (m_isLoop ? 0 : 1); i++)
            {
                Gizmos.DrawLine(m_railPoints[i].position, m_railPoints[(i + 1) % railCount].position);
            }
        }
    }
}