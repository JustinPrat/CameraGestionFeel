using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float Weight { get; set; }
    
    [field:SerializeField] public bool UsePlayerDirection { get; set; }
    
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
