using UnityEngine;

public static class PhysicsUtils
{
    public static bool IsLayerIsLayerMask(LayerMask layerMask, int layer)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
