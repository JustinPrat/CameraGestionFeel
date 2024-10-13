using System;
using UnityEngine;

public class AViewVolume : MonoBehaviour, IComparable<AViewVolume>
{
    [field:SerializeField] public int Priority { get; set; }

    [field: SerializeField, Range(0f, 1f)] public float WeightPercent { get; set; } = 1f;
    [field:SerializeField] public AView View { get; private set; }
    
    protected bool IsActive { get; private set; }

    public int UId { get; private set; }

    private static int NextUid;

    [SerializeField] private bool m_isCutOnSwitch;

    public virtual float ComputeSelfWeight(float remainingWeight)
    {
        return remainingWeight * WeightPercent;
    }

    protected virtual void Awake()
    {
        UId = NextUid++;
    }

    protected void SetActive(bool isActive)
    {
        if (IsActive == isActive || !View) return;
        IsActive = isActive;
        if (isActive)
        {
            ViewVolumeBlender.Instance.AddVolume(this);
            if (m_isCutOnSwitch)
            {
                ViewVolumeBlender.Instance.ApplyBlend();
                CameraController.Instance.Cut();
            }
        }
        else
        {
            ViewVolumeBlender.Instance.RemoveVolume(this);
        }
    }

    public int CompareTo(AViewVolume other)
    {
        int priorityDiff = -Priority.CompareTo(other.Priority);
        return priorityDiff != 0 ? priorityDiff : UId.CompareTo(other.UId);
    }
}
