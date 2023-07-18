using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Viewable : MonoBehaviour
{
    [SerializeField] private EView view;
    public EView View => view;
    
    public bool Active { get; private set; }

    public UnityEvent OnActivation;
    public UnityEvent OnDeactivation;

    private void Awake()
    {
        ViewManager.Instance.RegisterViewable(this);
    }

    public void Activate()
    {
        if (Active) return;
        Active = true;
        OnActivation.Invoke();
    }

    public void Deactivate()
    {
        if (!Active) return;
        Active = false;
        OnDeactivation.Invoke();
    }

    private void OnDestroy()
    {
        ViewManager.Instance.DeregisterViewable(this);
    }
}