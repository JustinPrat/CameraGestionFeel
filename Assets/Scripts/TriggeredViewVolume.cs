using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggeredViewVolume : AViewVolume
{
    [SerializeField]
    private LayerMask m_layerMask;
    
    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (PhysicsUtils.IsLayerIsLayerMask(m_layerMask, other.gameObject.layer))   
        {
            SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (PhysicsUtils.IsLayerIsLayerMask(m_layerMask, other.gameObject.layer))
        {
            SetActive(false);
        }
    }
}
