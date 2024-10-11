using UnityEngine;

public class FixedFollowView : AView
{
    [Header("Follow Parameters")]
    [SerializeField] private float m_roll;
    [SerializeField] private float m_FOV;
    [SerializeField] private Transform m_target;
    
    [Header("Constraint Parameters")]
    [SerializeField] private Transform m_centralPoint;
    [SerializeField] private float m_yawOffsetMax;
    [SerializeField] private float m_pitchOffsetMax;
    
    public override CameraConfiguration GetConfiguration()
    {
        Vector3 targetDirection = (m_target.position - transform.position).normalized;
        float targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        float targetPitch = -Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;
        if (m_centralPoint)
        {
            Vector3 centerDirection = (m_centralPoint.position - transform.position).normalized;
            float centerYaw = Mathf.Atan2(centerDirection.x, centerDirection.z) * Mathf.Rad2Deg;
            float centerPitch = -Mathf.Asin(centerDirection.y) * Mathf.Rad2Deg;
            targetPitch = Mathf.Clamp(targetPitch, centerPitch - m_pitchOffsetMax, centerPitch + m_pitchOffsetMax);
            targetYaw = centerYaw + Mathf.Clamp(Mathf.DeltaAngle(centerYaw, targetYaw), -m_yawOffsetMax, m_yawOffsetMax);
        }
        return new CameraConfiguration()
        {
            Roll = m_roll,
            FOV = m_FOV,
            Yaw = targetYaw,
            Pitch = targetPitch,
            Distance = 0,
            Pivot = transform.position,
        };
    }

    #if UNITY_EDITOR
    public void GenerateContraintObject()
    {
        if (!m_centralPoint)
        {
            m_centralPoint = new GameObject("CentralPoint").transform;
            m_centralPoint.parent = transform.parent;
            m_centralPoint.position = transform.position + Vector3.right;
        }
    }

    public void DestroyConstraintObject()
    {
        if (m_centralPoint)
        {
            DestroyImmediate(m_centralPoint.gameObject);
            m_centralPoint = null;
        }
    }
    #endif
}
