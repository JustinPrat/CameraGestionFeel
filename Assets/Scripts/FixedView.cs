using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FixedView : AView
{
    [SerializeField, Range(0, 179.99f)] private float m_yaw;
    [SerializeField, Range(-90, 90f)] private float m_pitch;
    [SerializeField, Range(-180, 180f)] private float m_roll;
    [SerializeField, Range(0f, 179.99f)] private float m_FOV = 90;

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration() { Yaw = m_yaw, Pitch = m_pitch, Roll = m_roll, FOV = m_FOV, Pivot = transform.position, Distance = 0 };
    }
}
