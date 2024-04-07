using UnityEngine;
using UnityEngine.Events;

public class SpringFloatDriver : FloatSpringListener
{
    public UnityEvent<float> OnValueChanged = new();
    
    protected override float GetOrig() => 1;

    protected override void ChangeValue(float value, float target)
    {
        OnValueChanged.Invoke(value);
    }
}