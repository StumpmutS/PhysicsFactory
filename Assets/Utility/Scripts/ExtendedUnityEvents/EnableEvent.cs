using UnityEngine;
using UnityEngine.Events;

public class EnableEvent : MonoBehaviour
{
    public UnityEvent OnEnabled = new();

    private void OnEnable()
    {
        OnEnabled.Invoke();
    }
}