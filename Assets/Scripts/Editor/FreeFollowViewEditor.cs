using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FreeFollowView)), CanEditMultipleObjects]
public class FreeFollowViewEditor : Editor
{
    private FreeFollowView m_freeFollowView;
    
    private void OnEnable()
    {
        m_freeFollowView = (FreeFollowView)target;
    }

    protected virtual void OnSceneGUI()
    {
        Matrix4x4 localToWorldMatrix = m_freeFollowView.LocalToWorldMatrix;
        Matrix4x4 worldToLocalMatrix = localToWorldMatrix.inverse;
        EditorGUI.BeginChangeCheck();
        Vector3 a = Handles.PositionHandle(localToWorldMatrix.MultiplyPoint(m_freeFollowView.Curve.A), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_freeFollowView, "Change FreeFollowView");
            m_freeFollowView.Curve.A = worldToLocalMatrix.MultiplyPoint(a);
            EditorUtility.SetDirty(m_freeFollowView);
        }
        EditorGUI.BeginChangeCheck();
        Vector3 b = Handles.PositionHandle(localToWorldMatrix.MultiplyPoint(m_freeFollowView.Curve.B), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_freeFollowView, "Change FreeFollowView");
            m_freeFollowView.Curve.B = worldToLocalMatrix.MultiplyPoint(b);
            EditorUtility.SetDirty(m_freeFollowView);
        }
        EditorGUI.BeginChangeCheck();
        Vector3 c = Handles.PositionHandle(localToWorldMatrix.MultiplyPoint(m_freeFollowView.Curve.C), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_freeFollowView, "Change FreeFollowView");
            m_freeFollowView.Curve.C = worldToLocalMatrix.MultiplyPoint(c);
            EditorUtility.SetDirty(m_freeFollowView);
        }
        EditorGUI.BeginChangeCheck();
        Vector3 d = Handles.PositionHandle(localToWorldMatrix.MultiplyPoint(m_freeFollowView.Curve.D), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_freeFollowView, "Change FreeFollowView");
            m_freeFollowView.Curve.D = worldToLocalMatrix.MultiplyPoint(d);
            EditorUtility.SetDirty(m_freeFollowView);
        }
    }
}