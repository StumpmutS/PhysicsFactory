using UnityEngine;

namespace Utility.Scripts
{
    public static class LayerHelper
    {
        public static bool LayerEqualsMask(int layer, LayerMask layerMask)
        {
            return (1 << layer & layerMask) != 0;
        }
    }
}