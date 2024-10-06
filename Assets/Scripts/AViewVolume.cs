using UnityEngine;

public class AViewVolume : MonoBehaviour
{
    [field:SerializeField]
    public int Priority { get; set; }
    [field:SerializeField]
    public AView View { get; set; }

    protected bool IsActive { get; private set; } = false;

    private int m_uId;

    private static int NextUid = 0;

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }

    private void Awake()
    {
        m_uId = NextUid++;
    }

    protected void SetActive(bool isActive)
    {
        if (IsActive == isActive) return;
        IsActive = isActive;
        if (isActive)
        {
            ViewVolumeBlender.Instance.AddVolume(this);
        }
        else
        {
            ViewVolumeBlender.Instance.RemoveVolume(this);
        }
    }
}
