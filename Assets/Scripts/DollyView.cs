using UnityEngine;

public class DollyView : AView
{
    [SerializeField] private float m_roll;
    [SerializeField] private float m_distance;
    [SerializeField] private float m_FOV;
    [SerializeField] private Transform m_target;
    [SerializeField] private Rail m_rail;
    [SerializeField, Min(0.01f)] private float m_speed;
    
    private float m_distanceOnRail;

    private void Update()
    {
        m_distanceOnRail += Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
    }

    public override CameraConfiguration GetConfiguration()
    {
        Vector3 position = m_rail?.GetPosition(m_distanceOnRail) ?? transform.position;
        Vector3 targetDirection = ((m_target?.position ?? transform.position) - transform.position).normalized;
        float targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        float targetPitch = -Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;
        return new CameraConfiguration()
        {
            Roll = m_roll,
            FOV = m_FOV,
            Yaw = targetYaw,
            Pitch = targetPitch,
            Distance = m_distance,
            Pivot = position,
        };
    }

    public override void SetupConfiguration()
    {
        m_roll = transform.rotation.eulerAngles.x;
    }
}