using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ForceTorque")]
public class ForceTorqueEffect : Effect
{
    [SerializeField] private float magnitude;
    
    public override void ApplyEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<Rigidbody>(out var rigidbody) ||
            !data.GameObject.TryGetComponent<Collider>(out var collider)) return;

        var point = collider.ClosestPoint(data.EffectOrigin);
        var direction = (point - data.EffectOrigin).normalized;

        rigidbody.AddForceAtPosition(direction * magnitude, point, ForceMode.Impulse);
    }

    public override void RemoveEffect(EffectData data) { }
}