using UnityEngine;
using UnityEngine.Events;

public class ViewMenuToggleInvoker : MonoBehaviour
{
    [SerializeField] private EView view;

    public UnityEvent<EView> OnActivate;
    
    public void Activate(bool value)
    {
        if (!value) return;
        OnActivate.Invoke(view);
    }
}