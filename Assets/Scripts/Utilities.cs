using UnityEngine;

public class Utilites
{
    public static bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
