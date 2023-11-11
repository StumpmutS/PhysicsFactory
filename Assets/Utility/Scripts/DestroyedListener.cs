using UnityEngine;
using UnityEngine.Events;

public class DestroyedListener : MonoBehaviour
{
    public UnityEvent<DestroyedListener> OnDestroyed = new();

    private void OnDestroy()
    {
        OnDestroyed.Invoke(this);
    }
}