using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public Transform Target;
    [Min(0)] public float OuterRadius;
    [Min(0)] public float InnerRadius;

    private float m_distance;

    private void Update()
    {
        m_distance = Vector3.Distance(transform.position, Target.position);
        if (m_distance <= OuterRadius && !IsActive)
        {
            SetActive(true);
        }
        else if (m_distance > OuterRadius && IsActive)
        {
            SetActive(false);
        }
    }

    public override float ComputeSelfWeight(float remainingWeight)
    {
        float deltaRadius = OuterRadius - InnerRadius;
        if (m_distance <= InnerRadius || deltaRadius == 0) 
        {
            return base.ComputeSelfWeight(remainingWeight);
        }
        else
        {
            return base.ComputeSelfWeight(remainingWeight) * ((m_distance - InnerRadius) / deltaRadius);
        }
    }

    private void OnValidate()
    {
        if (OuterRadius < InnerRadius)
        {
            OuterRadius = InnerRadius;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.1f, 0.2f, 0.4f);
        Gizmos.DrawSphere(transform.position, InnerRadius);
        Gizmos.color = new Color(0.1f, 0.5f, 0.6f, 0.2f);
        Gizmos.DrawSphere(transform.position, OuterRadius);
    }
}
