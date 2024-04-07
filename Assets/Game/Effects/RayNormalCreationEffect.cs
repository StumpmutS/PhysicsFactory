using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Creation/RayNormal")]
public class RayNormalCreationEffect : Effect
{
    [SerializeField] private Creatable creatablePrefab;

    public override void ApplyEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<Collider>(out var collider)) return;

        var point = collider.ClosestPoint(data.Origin);
        var ray = new Ray(data.Origin, (point - data.Origin).normalized);
        if (!collider.Raycast(ray, out var hit, 100f)) return;
        
        SceneCreatableReference.Instance.CreatableController.Create(
            creatablePrefab, data.GameObject, point, Quaternion.LookRotation(hit.normal));
    }

    public override void RemoveEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<CreatableController>(out var controller)) return;
        
        controller.Dispose(creatablePrefab, data.GameObject);
    }
}
