using UnityEngine;

[CreateAssetMenu(menuName = "GameResources/Modifiers/Negative")]
public class NegativeResourceModifier : ResourceModifier
{
    public override void Modify(ResourceData resourceData)
    {
        resourceData.Amount = -Mathf.Abs(resourceData.Amount);
    }
}