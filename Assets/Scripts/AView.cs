using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Min(1f)]
    public float Weight = 1f;
    public bool IsActiveOnStart;

    public abstract CameraConfiguration GetConfiguration();

    private void Start()
    {
        SetupConfiguration();
        SetActive(IsActiveOnStart);
    }

    public abstract void SetupConfiguration();
    
    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            CameraController.Instance.AddView(this);
        }
        else
        {
            CameraController.Instance.RemoveView(this);
        }
    }
    
    private void OnDrawGizmos()
    {
        GetConfiguration().DrawGizmos(Color.blue);
    }
}
