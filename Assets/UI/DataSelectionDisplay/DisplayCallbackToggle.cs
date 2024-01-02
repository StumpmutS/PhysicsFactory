using UnityEngine;

public abstract class DisplayCallbackToggle<TDisplayData> : MonoBehaviour
{
    [SerializeField] private CallbackToggle callbackToggle;

    public void Init(TDisplayData displayData, CallbackToggleData callbackToggleData)
    {
        callbackToggle.Init(callbackToggleData);
        DisplayData(displayData);
    }

    protected abstract void DisplayData(TDisplayData displayData);
}