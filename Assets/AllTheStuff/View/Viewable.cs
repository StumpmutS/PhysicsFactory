using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Viewable : MonoBehaviour
{
    [SerializeField] private EView view;
    public EView View => view;

    public UnityEvent OnActivation;
    public UnityEvent OnDeactivation;

    private void Awake()
    {
        ViewManager.Instance.RegisterViewable(this);
    }

    public void Activate()
    {
        OnActivation.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivation.Invoke();
    }

    private void OnDestroy()
    {
        ViewManager.Instance.DeregisterViewable(this);
    }
}
