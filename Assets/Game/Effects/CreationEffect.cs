using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Creation")]
public class CreationEffect : Effect
{
    [SerializeField] private Creatable creatablePrefab;
    
    public override void ApplyEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<CreatableController>(out var controller)) return;
        
        controller.Create(creatablePrefab, this);
    }

    public override void RemoveEffect(EffectData data)
    {
        if (!data.GameObject.TryGetComponent<CreatableController>(out var controller)) return;
        
        controller.Dispose(creatablePrefab, this);
    }
}