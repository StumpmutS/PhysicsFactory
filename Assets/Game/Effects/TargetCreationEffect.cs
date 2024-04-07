using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Creation/Target")]
public class TargetCreationEffect : Effect
{
    [SerializeField] private Creatable creatablePrefab;
    
    public override void ApplyEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<CreatableController>(out var controller)) return;
        
        controller.Create(creatablePrefab, data.GameObject);
    }

    public override void RemoveEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<CreatableController>(out var controller)) return;
        
        controller.Dispose(creatablePrefab, data.GameObject);
    }
}