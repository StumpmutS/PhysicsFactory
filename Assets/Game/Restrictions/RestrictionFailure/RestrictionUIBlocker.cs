using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class RestrictionUIBlocker : UIBlocker
{
    public void Init(ERestrictionFailureType failureType, GameObject blockingPrefab)
    {
        var restrictionFeedbackInfo = failureType.GetFlaggedValues()
            .Select(e => RestrictionFeedbackReference.Instance.GetFeedbackInfo(e));

        Init(GenerateBlockerInfo(restrictionFeedbackInfo, blockingPrefab));
    }

    private UIBlockerData GenerateBlockerInfo(IEnumerable<RestrictionFeedbackInfo> restrictionFeedbackInfos, GameObject blockingPrefab)
    {
        return new UIBlockerData(blockingPrefab, new UIBlockableData(restrictionFeedbackInfos.Select(r => r.Icon)));
    }
}