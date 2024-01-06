using UnityEngine;
using Utility.Scripts;

public class RestrictionFeedbackReference : Singleton<RestrictionFeedbackReference>
{
    [SerializeField] private SerializableDictionary<ERestrictionFailureType, RestrictionFeedbackInfo> failureInfo;

    public RestrictionFeedbackInfo GetFeedbackInfo(ERestrictionFailureType failureType)
    {
        return failureInfo[failureType.AsSingleFlag()];
    }
}