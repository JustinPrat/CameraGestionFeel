using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FixedFollowView)), CanEditMultipleObjects]
public class FixedFollowViewEditor : Editor
{
    private FixedFollowView m_Target;
    
    private void OnEnable()
    {
        m_Target = (FixedFollowView)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Contraint Object"))
        {
            m_Target.GenerateContraintObject();
        }
    }
}