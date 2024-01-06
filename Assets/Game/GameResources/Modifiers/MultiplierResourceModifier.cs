using UnityEngine;

[CreateAssetMenu(menuName = "GameResources/Modifiers/Multiplier")]
public class MultiplierResourceModifier : ResourceModifier
{
    [SerializeField] private float multiplier;
    
    public override void Modify(ResourceData resourceData)
    {
        resourceData.Amount *= multiplier;
    }
}