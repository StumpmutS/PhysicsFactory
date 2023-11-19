using UnityEngine;
using Utility.Scripts;

public class RestrictionFeedbackReference : Singleton<RestrictionFeedbackReference>
{
    [SerializeField] private SerializableDictionary<ERestrictionFailureType, RestrictionFeedbackInfo> failureInfo;
    public SerializableDictionary<ERestrictionFailureType, RestrictionFeedbackInfo> FailureInfo => failureInfo;
}