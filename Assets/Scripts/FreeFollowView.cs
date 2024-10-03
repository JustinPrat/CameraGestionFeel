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
    private float m_curvePosition = .5f;

    Matrix4x4 LocalToWorldMatrix => Matrix4x4.TRS(m_target.position, m_target.rotation, Vector3.one);

    protected override void Start()
    {
        base.Start();
        m_yaw = Vector3.Angle(m_target.position - m_curve.GetPosition(0.5f, LocalToWorldMatrix), Vector3.forward);
    }

    private void Update()
    {
        m_yaw += Input.GetAxis("Horizontal") * m_yawSpeed * Time.deltaTime;
        m_target.rotation = Quaternion.Euler(0, m_yaw, 0);
        m_curvePosition = Mathf.Clamp01(m_curvePosition + Input.GetAxis("Vertical") * m_curveSpeed * Time.deltaTime);
    }
    
    public override CameraConfiguration GetConfiguration()
    {
        FreeFollowPosition followPosition = (m_curvePosition < .5f) ? FreeFollowPosition.Lerp(m_bottomPosition, m_middlePosition, m_curvePosition * 2) 
            : FreeFollowPosition.Lerp(m_middlePosition, m_topPosition, m_curvePosition * 2 - 1f);
        return new CameraConfiguration()
        {
            FOV = followPosition.FOV,
            Pitch = followPosition.Pitch,
            Roll = followPosition.Roll,
            Pivot = m_curve.GetPosition(m_curvePosition, LocalToWorldMatrix),
            Yaw = m_yaw,
        };
    }

    public override void SetupConfiguration()
    {
    }

    private void OnDrawGizmos()
    {
        new CameraConfiguration()
        {
            FOV = m_bottomPosition.FOV,
            Pitch = m_bottomPosition.Pitch,
            Roll = m_bottomPosition.Roll,
            Pivot = m_curve.GetPosition(0f, LocalToWorldMatrix),
            Yaw = m_yaw,
        }.DrawGizmos(Color.green);  
        new CameraConfiguration()
        {
            FOV = m_middlePosition.FOV,
            Pitch = m_middlePosition.Pitch,
            Roll = m_middlePosition.Roll,
            Pivot = m_curve.GetPosition(0.5f, LocalToWorldMatrix),
            Yaw = m_yaw,
        }.DrawGizmos(Color.yellow);
        new CameraConfiguration()
        {
            FOV = m_topPosition.FOV,
            Pitch = m_topPosition.Pitch,
            Roll = m_topPosition.Roll,
            Pivot = m_curve.GetPosition(1f, LocalToWorldMatrix),
            Yaw = m_yaw,
        }.DrawGizmos(Color.red);
        m_curve.DrawGizmos(Color.blue, LocalToWorldMatrix);
    }
}
