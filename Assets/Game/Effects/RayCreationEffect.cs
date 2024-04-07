using UnityEngine;
using Utility.Scripts.Extensions;

[CreateAssetMenu(menuName = "Effects/Creation/Ray")]
public class RayCreationEffect : Effect
{
    [SerializeField] private Creatable creatablePrefab;

    public override void ApplyEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<Collider>(out var collider)) return;

        var destination = collider.ClosestPoint(data.Origin);
        var direction = destination - data.Origin;

        var created = SceneCreatableReference.Instance.CreatableController.Create(
            creatablePrefab, data.GameObject, data.Origin, 
            Quaternion.LookRotation(direction));
        created.transform.localScale = new Vector3(
            created.transform.localScale.x, created.transform.localScale.y, direction.magnitude);
    }

    public override void RemoveEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<CreatableController>(out var controller)) return;
        
        controller.Dispose(creatablePrefab, data.GameObject);
    }
}