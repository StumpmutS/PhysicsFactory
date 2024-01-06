using System.Collections.Generic;
using System.Linq;
using Utility.Scripts;

public class RestrictionUIBlocker : UIBlocker
{
    public void Init(ERestrictionFailureType failureType, IconData restrictedIcon)
    {
        var restrictionFeedbackInfo = failureType.GetFlaggedValues()
            .Select(e => RestrictionFeedbackReference.Instance.GetFeedbackInfo(e));
        
        Init(GenerateBlockerInfo(restrictionFeedbackInfo, restrictedIcon));
    }

    private UIBlockerInfo GenerateBlockerInfo(IEnumerable<RestrictionFeedbackInfo> restrictionFeedbackInfos, IconData restrictedIcon)
    {
        var feedbackIcons = new List<IconData>();
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