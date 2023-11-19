using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBlockableInfo
{
    public IconInfo MainIcon;
    public List<IconInfo> FeedbackIcons;

    public UIBlockableInfo(IconInfo mainIcon, List<IconInfo> feedbackIcons)
    {
        MainIcon = mainIcon;
        FeedbackIcons = feedbackIcons;
    }
}