using UnityEngine;

public class TimeScaleSetter : MonoBehaviour
{
    public void SetTimeScale(float value)
    {
        TimeScaleManager.Instance.SetTimeScale(this, value);
    }

    public void ResetTimeScale()
    {
        TimeScaleManager.Instance.ResetTimeScale(this);
    }
}