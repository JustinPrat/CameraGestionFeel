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
        base.OnInspectorGUI();
        if (GUILayout.Button("Setup Configuration"))
        {
            m_Target.SetupConfiguration();
        }
    }
}