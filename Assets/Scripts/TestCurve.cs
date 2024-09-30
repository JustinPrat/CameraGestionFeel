using System;
using UnityEngine;

public class TestCurve : MonoBehaviour
{
    [SerializeField] private Curve m_curve;

    private void OnDrawGizmosSelected()
    {
        m_curve?.DrawGizmos(Color.green, transform.localToWorldMatrix);
    }
}
