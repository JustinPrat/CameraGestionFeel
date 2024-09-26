using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDisplayGizmo : MonoBehaviour
{
    [SerializeField] private Camera m_targetCamera;
    
    private void Reset()
    {
        m_targetCamera = GetComponent<Camera>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // Gizmos.DrawSphere(Pivot, 0.25f);
        Vector3 position = m_targetCamera.transform.position;
        // Gizmos.DrawLine(Pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, m_targetCamera.transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, m_targetCamera.fieldOfView, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
