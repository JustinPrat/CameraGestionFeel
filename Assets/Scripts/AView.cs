using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Min(1f)]
    public float Weight = 1f;

    public abstract CameraConfiguration GetConfiguration();
    
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
