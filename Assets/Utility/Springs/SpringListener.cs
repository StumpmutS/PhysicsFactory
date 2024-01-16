using UnityEngine;

[RequireComponent(typeof(SpringController))]
public abstract class SpringListener : MonoBehaviour
{
    [SerializeField] protected bool useSetValue;
    [SerializeField, ShowIf(nameof(useSetValue), false)] protected float minMultiplier, maxMultiplier;

    public void TryHandleSpringValue(float amount, float target)
    {
        if (enabled) HandleSpringValue(amount, target);
    }

    protected abstract void HandleSpringValue(float amount, float target);
}
