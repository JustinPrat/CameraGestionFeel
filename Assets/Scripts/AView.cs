using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Min(1f)]
    public float Weight = 1f;

    public abstract CameraConfiguration GetConfiguration();

    protected virtual void Start()
    {
        SetupConfiguration();
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
