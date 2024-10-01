using System;
using UnityEngine;

public class FreeFollowView : AView
{
    [Serializable]
    public struct FreeFollowPosition
    {
        public float Pitch;
        public float Roll;
        public float FOV;

        public static FreeFollowPosition Lerp(FreeFollowPosition from, FreeFollowPosition to, float t)
        {
            t = Mathf.Clamp01(t);
            return new FreeFollowPosition()
            {
                Pitch = Mathf.Lerp(from.Pitch, to.Pitch, t),
                Roll = Mathf.Lerp(from.Roll, to.Roll, t),
                FOV = Mathf.Lerp(from.FOV, to.FOV, t),
            };
        }
    }
    
    [Header("Positions")]
    [SerializeField] private FreeFollowPosition m_bottomPosition;
    [SerializeField] private FreeFollowPosition m_middlePosition;
    [SerializeField] private FreeFollowPosition m_topPosition;
    
    [Header("Parameters")]
    [SerializeField] private Curve m_curve;
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_yawSpeed;
    [SerializeField] private float m_curveSpeed;

    private float m_yaw;
    [SerializeField, Range(0f, 1f)] private float m_curvePosition;
    
    public override CameraConfiguration GetConfiguration()
    {
        float yaw = Vector3.SignedAngle(m_target.position - m_curve.GetPosition(0.5f, m_target.localToWorldMatrix), -m_target.transform.forward, Vector3.up);
        FreeFollowPosition followPosition = (m_curvePosition < .5f) ? FreeFollowPosition.Lerp(m_bottomPosition, m_middlePosition, m_curvePosition * 2) 
            : FreeFollowPosition.Lerp(m_middlePosition, m_topPosition, m_curvePosition * 2 - 1f);
        return new CameraConfiguration()
        {
            FOV = followPosition.FOV,
            Pitch = followPosition.Pitch,
            Roll = followPosition.Roll,
            Pivot = m_curve.GetPosition(m_curvePosition, m_target.localToWorldMatrix),
            Yaw = yaw,
        };
    }

    public override void SetupConfiguration()
    {
    }

    private void OnDrawGizmosSelected()
    {
        float yaw = Vector3.SignedAngle(m_target.position - m_curve.GetPosition(0.5f, m_target.localToWorldMatrix), -m_target.transform.forward, Vector3.up);
        new CameraConfiguration()
        {
            FOV = m_bottomPosition.FOV,
            Pitch = m_bottomPosition.Pitch,
            Roll = m_bottomPosition.Roll,
            Pivot = m_curve.GetPosition(0f, m_target.localToWorldMatrix),
            Yaw = yaw,
        }.DrawGizmos(Color.green);  
        new CameraConfiguration()
        {
            FOV = m_middlePosition.FOV,
            Pitch = m_middlePosition.Pitch,
            Roll = m_middlePosition.Roll,
            Pivot = m_curve.GetPosition(0.5f, m_target.localToWorldMatrix),
            Yaw = yaw,
        }.DrawGizmos(Color.yellow);
        new CameraConfiguration()
        {
            FOV = m_topPosition.FOV,
            Pitch = m_topPosition.Pitch,
            Roll = m_topPosition.Roll,
            Pivot = m_curve.GetPosition(1f, m_target.localToWorldMatrix),
            Yaw = yaw,
        }.DrawGizmos(Color.red);
        m_curve.DrawGizmos(Color.blue, m_target.localToWorldMatrix);
    }
}
