using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    public static ViewVolumeBlender Instance;
    
    private List<AViewVolume> m_activeViewVolumes = new List<AViewVolume>();
    private Dictionary<AView, List<AViewVolume>> m_volumePerViews = new Dictionary<AView, List<AViewVolume>>();


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

    private void Update()
    {
        ApplyBlend();
    }

    public void ApplyBlend ()
    {
        List<AViewVolume> allVolumes = new List<AViewVolume>();
        foreach (AViewVolume viewVolume in m_activeViewVolumes)
        {
            viewVolume.View.Weight = 0;
            allVolumes.Add(viewVolume);
        }
        allVolumes.Sort();
        float totalWeight = 1f;
        foreach (AViewVolume volume in allVolumes)
        {
            volume.View.Weight = volume.ComputeSelfWeight(totalWeight);
            totalWeight -= volume.View.Weight;
        }
        totalWeight = 1f - totalWeight;
        if (totalWeight <= 0f)
        {
            throw new DivideByZeroException("No volume with weight has been found in the scene, make sure there is one before playing to avoid division by zero.");
        }
        foreach (AViewVolume volume in allVolumes)
        {
            volume.View.Weight *= totalWeight;
        }
    }

    public void AddVolume(AViewVolume volume)
    {
        m_activeViewVolumes.Add(volume);
        if (m_volumePerViews.TryGetValue(volume.View, out List<AViewVolume> volumesForView))
        {
            volumesForView.Add(volume);
        }
        else
        {
            m_volumePerViews.Add(volume.View, new List<AViewVolume>() { volume });
            volume.View.SetActive(true);
        }
    }    
    
    public void RemoveVolume(AViewVolume volume)
    {
        m_activeViewVolumes.Remove(volume);
        if (m_volumePerViews.TryGetValue(volume.View, out List<AViewVolume> volumesForView))
        {
             volumesForView.Remove(volume);
             if (volumesForView.Count == 0)
             {
                 volume.View.SetActive(false);
                 m_volumePerViews.Remove(volume.View);
             }
        }
    }

    private void OnGUI()
    {
        foreach (AViewVolume volume in m_activeViewVolumes)
        {
            GUILayout.Label(volume.GetType().Name + " : " +volume.name);
        }
    }
}
