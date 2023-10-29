using UnityEngine;

public static class MaterialHelper
{
    public static bool IsZWriteMaterial(Material material, out bool zWrite)
    {
        zWrite = false;
        
        if (material.name.StartsWith("ZWOn"))
        {
            zWrite = true;
            return true;
        }

        if (material.name.StartsWith("ZWOff"))
        {
            zWrite = false;
            return true;
        }
        
        return false;
    }
}