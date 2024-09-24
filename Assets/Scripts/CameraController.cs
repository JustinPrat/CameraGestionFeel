using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Camera Camera;
    public CameraConfiguration CameraConfiguration;

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

    private void ApplyConfiguration()
    {
        Camera.transform.rotation = CameraConfiguration.GetRotation();
        Camera.transform.position = CameraConfiguration.GetPosition();
    }

    private void Update()
    {
        ApplyConfiguration();
    }

    private void OnDrawGizmos()
    {
        CameraConfiguration.DrawGizmos(Color.red);
    }
}
