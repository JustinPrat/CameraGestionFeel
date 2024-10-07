using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Camera Camera;
    private CameraConfiguration m_cameraConfiguration;
    private CameraConfiguration m_cameraTarget;
    private List<AView> m_activeViews = new List<AView>();
    [SerializeField] private float timer;

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
        m_cameraConfiguration = ComputeAverage();
        m_cameraTarget = ComputeAverage();
    }

    public CameraConfiguration ComputeAverage ()
    {
        CameraConfiguration cameraConfig = new CameraConfiguration();
        float totalWeight = 0;
        Vector2 yawSum = new Vector2();
        foreach (AView view in m_activeViews)
        {
            totalWeight += view.Weight;
            CameraConfiguration current = view.GetConfiguration();
            cameraConfig.Distance += current.Distance * view.Weight;
            cameraConfig.FOV += current.FOV * view.Weight;
            cameraConfig.Pivot += current.Pivot * view.Weight;
            cameraConfig.Pitch += current.Pitch * view.Weight;
            cameraConfig.Roll += current.Roll * view.Weight;
            yawSum += new Vector2(Mathf.Cos(current.Yaw * Mathf.Deg2Rad),Mathf.Sin(current.Yaw * Mathf.Deg2Rad)) * view.Weight;
        }

        cameraConfig.Distance /= totalWeight;
        cameraConfig.FOV /= totalWeight;
        cameraConfig.Pivot /= totalWeight;
        cameraConfig.Pitch /= totalWeight;
        cameraConfig.Roll /= totalWeight;
        cameraConfig.Yaw = Vector2.SignedAngle(Vector2.right, yawSum);
        return cameraConfig;
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

    private void Update()
    {
        timer += Time.deltaTime;

        //if (timer / )
        //    m_cameraConfiguration = m_cameraConfiguration + (m_cameraConfiguration - m_cameraTarget) * timer * Time.deltaTime;
        //else
        //    m_cameraConfiguration = m_cameraTarget;

        ApplyConfiguration();
        m_cameraTarget = ComputeAverage();
    }

    private void OnDrawGizmos()
    {
        m_cameraConfiguration.DrawGizmos(Color.red);
    }
}
