using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    public float Yaw;
    public float Pitch;
    public float Roll;
    public float FOV;

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration() { Yaw = Yaw, Pitch = Pitch, Roll = Roll, FOV = FOV, Pivot = transform.position, Distance = 0 };
    }
}
