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
        ApplyConfiguration();
    }

    private void OnDrawGizmos()
    {
        m_cameraConfiguration.DrawGizmos(Color.red);
    }
}
