using System;
using UnityEngine;
using UnityEngine.Events;

public class Isolatable : MonoBehaviour
{
    public UnityEvent<int> OnIsolate;
    public UnityEvent OnDeIsolate;

    private void Start()
    {
        IsolationManager.Instance.AddIsolatable(this);
    }

    private void OnDestroy()
    {
        IsolationManager.Instance.RemoveIsolatable(this);
    }

    public void Isolate(int yLevel)
    {
        OnIsolate.Invoke(yLevel);
    }

    public void DeIsolate()
    {
        OnDeIsolate.Invoke();
    }
}