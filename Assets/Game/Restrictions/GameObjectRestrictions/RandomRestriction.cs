using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Restrictions/GameObject/Random")]
public class RandomRestriction : GameObjectRestriction
{
    [SerializeField] private float sampleInterval;
    [SerializeField, Range(0f, 1f)] private float successChance;
    
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.None;
    
    protected override bool Check(GameObject _, RestrictionFailureInfo failureInfo)
    {
        var windowedTime = Time.time % (sampleInterval * 2);
        return windowedTime - Time.deltaTime < sampleInterval && sampleInterval <= windowedTime 
               && Random.Range(0f, 1f) <= successChance;
    }
}