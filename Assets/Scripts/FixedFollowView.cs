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
        Vector3 direction = (m_target.position - transform.position).normalized;
        // Vector3 centralPointYaw = 
        // float angle = Vector3.Angle(m_centralPoint.position - transform.position, direction);
        return new CameraConfiguration()
        {
            Roll = m_roll,
            FOV = m_FOV,
            // Yaw = Mathf.Clamp(Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, ),
            Pitch = -Mathf.Asin(direction.y) * Mathf.Rad2Deg,
            Distance = 0,
            Pivot = transform.position,
        };
    }
    
    public override void SetupConfiguration()
    {
        m_roll = transform.rotation.eulerAngles.x;
    }
}