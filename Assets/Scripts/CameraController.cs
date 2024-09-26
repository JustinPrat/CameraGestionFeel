using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField]
    private Camera m_camera;
    
    [Header("Smoothing Settings")]
    [SerializeField, Range(0f, 100f)] private float m_smoothingSpeed = 0.5f;
    [SerializeField, Min(0f)] private float m_minimumDistance = 0.01f;
    
    private CameraConfiguration m_targetCameraConfiguration;
    private CameraConfiguration m_currentCameraConfiguration;
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

    private void Start()
    {
        m_currentCameraConfiguration = new CameraConfiguration()
        {
            FOV = m_camera.fieldOfView,
            Roll = m_camera.transform.eulerAngles.x,
            Yaw = m_camera.transform.eulerAngles.y,
            Pitch = m_camera.transform.eulerAngles.z,
            Pivot = m_camera.transform.position,
            Distance = 0f,
        };
    }

    private void Update()
    {
        m_targetCameraConfiguration = ComputeAverage();
        if (m_smoothingSpeed * Time.deltaTime < 1)
        {
            m_currentCameraConfiguration = new CameraConfiguration()
            {
                FOV = m_currentCameraConfiguration.FOV + (m_targetCameraConfiguration.FOV - m_currentCameraConfiguration.FOV) * (Time.deltaTime * m_smoothingSpeed),
                Roll = m_currentCameraConfiguration.Roll + (m_targetCameraConfiguration.Roll - m_currentCameraConfiguration.Roll) * (Time.deltaTime * m_smoothingSpeed),
                Pitch = m_currentCameraConfiguration.Pitch + (m_targetCameraConfiguration.Pitch - m_currentCameraConfiguration.Pitch) * (Time.deltaTime * m_smoothingSpeed),
                Yaw = m_currentCameraConfiguration.Yaw + (m_targetCameraConfiguration.Yaw - m_currentCameraConfiguration.Yaw) * (Time.deltaTime * m_smoothingSpeed),
                Distance = m_currentCameraConfiguration.Distance + (m_targetCameraConfiguration.Distance - m_currentCameraConfiguration.Distance) * (Time.deltaTime * m_smoothingSpeed),
                Pivot = m_currentCameraConfiguration.Pivot + (m_targetCameraConfiguration.Pivot - m_currentCameraConfiguration.Pivot) * (Time.deltaTime * m_smoothingSpeed),
            };
        }
        else
        {
            m_currentCameraConfiguration = m_targetCameraConfiguration;
        }
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
        m_camera.transform.rotation = m_currentCameraConfiguration.GetRotation();
        m_camera.transform.position = m_currentCameraConfiguration.GetPosition();
        m_camera.fieldOfView = m_currentCameraConfiguration.FOV;
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
        cameraConfig.Yaw = Vector2.SignedAngle(Vector2.right, sumYaw);
        cameraConfig.Distance /= weightTotal;
        cameraConfig.FOV /= weightTotal;
        cameraConfig.Pivot /= weightTotal;
        cameraConfig.Pitch /= weightTotal;
        cameraConfig.Roll /= weightTotal;
        return cameraConfig;
    }

    private void OnDrawGizmos()
    {
        m_targetCameraConfiguration.DrawGizmos(Color.red);
    }
}
