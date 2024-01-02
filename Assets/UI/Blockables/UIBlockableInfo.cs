using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBlockableInfo
{
    public IconData MainIcon;
    public List<IconData> FeedbackIcons;

    public UIBlockableInfo(IconData mainIcon, List<IconData> feedbackIcons)
    {
        MainIcon = mainIcon;
        FeedbackIcons = feedbackIcons;
    }
}