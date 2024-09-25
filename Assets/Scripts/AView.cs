using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float Weight;
    public bool IsActiveOnStart;

    public abstract CameraConfiguration GetConfiguration();

    private void Start()
    {
        SetActive(IsActiveOnStart);
    }


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
}
