using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts.Extensions;

public class PoppableStack : MonoBehaviour
{
    private LinkedList<IPoppable> _poppables = new();

    public UnityEvent OnStackAvailable = new();

    public void TryPop()
    {
        if (_poppables.TryPopLast(out var poppable))
        {
            poppable.Pop();
            return;
        }

        OnStackAvailable.Invoke();
    }

    public void RegisterPoppable(IPoppable poppable)
    {
        _poppables.Remove(poppable);
        _poppables.AddLast(poppable);
    }

    public void DeregisterPoppable(IPoppable poppable)
    {
        _poppables.Remove(poppable);
    }
}