using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Camera Camera;
    
    private CameraConfiguration m_cameraConfiguration;
    private List<AView> m_activeViews = new List<AView>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

        
    private void Update()
    {
        m_cameraConfiguration = ComputeAverage();
    }
    
    private void LateUpdate()
    {
        ApplyConfiguration();
    }
    
    public void AddView(AView view)
    {
        if (!m_activeViews.Contains(view))
        {
            m_activeViews.Add(view);
        }
    }

    public void RemoveView(AView view)
    {
        m_activeViews.Remove(view);
    }

    private void ApplyConfiguration()
    {
        Camera.transform.rotation = m_cameraConfiguration.GetRotation();
        Camera.transform.position = m_cameraConfiguration.GetPosition();
    }

    private CameraConfiguration ComputeAverage()
    {
        if (m_activeViews.Count == 0)
        {
            return new CameraConfiguration();
        }
        CameraConfiguration cameraConfig = new CameraConfiguration();
        float weightTotal = 0;
        Vector2 sumYaw = Vector2.zero;
        foreach (AView view in m_activeViews)
        {
            CameraConfiguration viewConfig = view.GetConfiguration();
            weightTotal += view.Weight;
            cameraConfig.Distance += viewConfig.Distance * view.Weight;
            cameraConfig.FOV += viewConfig.FOV * view.Weight;
            cameraConfig.Pivot += viewConfig.Pivot * view.Weight;
            cameraConfig.Pitch += viewConfig.Pitch * view.Weight;
            cameraConfig.Roll += viewConfig.Roll * view.Weight;
            sumYaw += new Vector2(Mathf.Cos(viewConfig.Yaw * Mathf.Deg2Rad),
                Mathf.Sin(viewConfig.Yaw * Mathf.Deg2Rad)) * view.Weight;
        }
        cameraConfig.Yaw = Vector2.SignedAngle(Vector2.right, sumYaw) / weightTotal;
        cameraConfig.Distance /= weightTotal;
        cameraConfig.FOV /= weightTotal;
        cameraConfig.Pivot /= weightTotal;
        cameraConfig.Pitch /= weightTotal;
        cameraConfig.Roll /= weightTotal;
        return cameraConfig;
    }

    private void OnDrawGizmos()
    {
        m_cameraConfiguration.DrawGizmos(Color.red);
    }
}
