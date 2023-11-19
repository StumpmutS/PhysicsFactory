using System.Collections.Generic;
using System.Linq;
using Utility.Scripts;

public class RestrictionUIBlocker : UIBlocker
{
    public void Init(ERestrictionFailureType failureType, IconInfo restrictedIcon)
    {
        var restrictionFeedbackInfo = failureType.GetFlaggedValues().Select(e => RestrictionFeedbackReference.Instance.FailureInfo[e]);
        
        Init(GenerateBlockerInfo(restrictionFeedbackInfo, restrictedIcon));
    }

    private UIBlockerInfo GenerateBlockerInfo(IEnumerable<RestrictionFeedbackInfo> restrictionFeedbackInfos, IconInfo restrictedIcon)
    {
        var feedbackIcons = new List<IconInfo>();
        var blockableInfo = new UIBlockableInfo(restrictedIcon, feedbackIcons);
        var componentAdders = new HashSet<ComponentAdder>();
        var blockerInfo = new UIBlockerInfo(blockableInfo, componentAdders);
        foreach (var restrictionFeedbackInfo in restrictionFeedbackInfos)
        {
            feedbackIcons.Add(restrictionFeedbackInfo.Icon);
            foreach (var componentAdder in restrictionFeedbackInfo.ComponentAdders)
            {
                componentAdders.Add(componentAdder);
            }
        }

        return blockerInfo;
    }
}