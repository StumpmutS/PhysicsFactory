using UnityEngine;
using UnityEngine.Events;

public class DisableEvent : MonoBehaviour
{
    public UnityEvent OnDisabled = new();

    private void OnDisable()
    {
        OnDisabled.Invoke();
    }
}