using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

[Serializable]
public class UIBlockerInfo
{
    public UIBlockableInfo BlockableInfo;
    public HashSet<ComponentAdder> ComponentAdders;
    
    public UIBlockerInfo(UIBlockableInfo blockableInfo, HashSet<ComponentAdder> componentAdders)
    {
        BlockableInfo = blockableInfo;
        ComponentAdders = componentAdders;
    }
}