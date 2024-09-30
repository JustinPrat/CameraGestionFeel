using UnityEngine;

public class FreeFollowView : AView
{
    [SerializeField] private Curve m_curve;
    
    public override CameraConfiguration GetConfiguration()
    {
        // TO DO
        return new CameraConfiguration();
    }

    public override void SetupConfiguration()
    {
    }
}
