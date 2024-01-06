using System;
using UnityEngine;
using Utility.Scripts;

public class RestrictionFailureDisplay : Singleton<RestrictionFailureDisplay>
{
    [SerializeField] private TextFlasher flasher;
    
    public void DisplayFailure(RestrictionFailureInfo failureInfo)
    {
        if (!failureInfo.Failed) return;

        var text = RestrictionFeedbackReference.Instance.GetFeedbackInfo(failureInfo.FailureType).Text;
        flasher.Flash(text);
    }
}
