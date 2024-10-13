using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Rail))]
public class RailEditor : Editor
{
    private Rail m_rail;

    private void OnEnable()
    {
        m_rail = (Rail)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate New Point"))
        {
            m_rail.GenerateNewRailPoint();
        }
    }
}
