using System.Collections.Generic;
using System.Linq;

public class UIBlockableData
{
    public List<IconData> FeedbackIcons;

    public UIBlockableData(IEnumerable<IconData> feedbackIcons)
    {
        FeedbackIcons = feedbackIcons.ToList();
    }
}