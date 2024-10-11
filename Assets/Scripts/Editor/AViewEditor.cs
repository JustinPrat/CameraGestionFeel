using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FixedFollowView)), CanEditMultipleObjects]
public class FixedFollowViewEditor : Editor
{
    private FixedFollowView m_target;
    
    private void OnEnable()
    {
        m_target = (FixedFollowView)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Contraint Object"))
        {
            m_target.GenerateContraintObject();
        }
        if (GUILayout.Button("Destroy Contraint Object"))
        {
            m_target.DestroyConstraintObject();
        }
    }
}