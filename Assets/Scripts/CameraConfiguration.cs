using System;
using UnityEngine;

[Serializable]
public struct CameraConfiguration 
{
    [field:SerializeField]
    public float Yaw { get; set; }

    [field: SerializeField]
    public float Pitch { get; set; }

    [field: SerializeField]
    public float Roll { get; set; }

    [field: SerializeField]
    public Vector3 Pivot { get; set; }

    [field: SerializeField]
    public float Distance { get; set; }

    [field: SerializeField]
    public float FOV { get; set; }

    public Quaternion GetRotation() => Quaternion.Euler(Pitch, Yaw, Roll);

    public Vector3 GetPosition() => Pivot + GetRotation() * (Vector3.back * Distance);

    public static CameraConfiguration operator - (CameraConfiguration left, CameraConfiguration right)
    {
        left.Yaw = Mathf.DeltaAngle(left.Yaw, right.Yaw);
        left.Pitch -= right.Pitch;
        left.Roll -= right.Roll;
        left.Distance -= right.Distance;
        left.FOV -= right.FOV;
        left.Pivot -= right.Pivot;
        return left;
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(Pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(Pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, FOV, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
