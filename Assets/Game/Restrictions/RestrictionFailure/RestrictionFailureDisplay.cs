using UnityEngine;
using Utility.Scripts;

public class RestrictionFailureDisplay : Singleton<RestrictionFailureDisplay>
{
    [SerializeField] private TextFlasher flasher;
    
    public void DisplayFailure(RestrictionFailureInfo failureInfo)
    {
        if (!failureInfo.Failed) return;

        var text = RestrictionFeedbackReference.Instance.FailureInfo[failureInfo.FailureType].Text;
        flasher.Flash(text);
    }
}
