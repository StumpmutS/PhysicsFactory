using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class UIBlocker : MonoBehaviour, IInitializableComponent<UIBlockerInfo>
{
    private HashSet<IUIBlockable> _blockables;

    public void Init(UIBlockerInfo info)
    {
        foreach (var componentAdder in info.ComponentAdders)
        {
            componentAdder.AddOrGetTo(gameObject);
        }
        
        _blockables = GetComponentsInChildren<IUIBlockable>().ToHashSet();
        foreach (var blockable in _blockables)
        {
            blockable.Block(info.BlockableInfo);
        }
    }

    private void OnDestroy()
    {
        foreach (var blockable in _blockables)
        {
            blockable.Unblock();
        }
    }
}