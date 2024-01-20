using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class TimedHoverEvent : AwaitEvent
{
    [SerializeField] private PointerEvents pointerEvents;

    private void Awake()
    {
        if (pointerEvents == null) pointerEvents = this.AddOrGetComponent<PointerEvents>();
        
        pointerEvents.OnPointerEnterDown.AddListener(ResetOrBeginWait);
        pointerEvents.OnPointerEnterUp.AddListener(ResetOrBeginWait);
        pointerEvents.OnPointerExitDown.AddListener(StopWait);
        pointerEvents.OnPointerExitUp.AddListener(StopWait);
        pointerEvents.OnPointerDownEntered.AddListener(ResetOrBeginWait);
        pointerEvents.OnPointerUpEntered.AddListener(ResetOrBeginWait);
    }

    private void OnDestroy()
    {
        if (pointerEvents != null) pointerEvents.OnPointerEnterDown.RemoveListener(ResetOrBeginWait);
        if (pointerEvents != null) pointerEvents.OnPointerEnterUp.RemoveListener(ResetOrBeginWait);
        if (pointerEvents != null) pointerEvents.OnPointerExitDown.RemoveListener(StopWait);
        if (pointerEvents != null) pointerEvents.OnPointerExitUp.RemoveListener(StopWait);
        if (pointerEvents != null) pointerEvents.OnPointerDownEntered.RemoveListener(ResetOrBeginWait);
        if (pointerEvents != null) pointerEvents.OnPointerUpEntered.RemoveListener(ResetOrBeginWait);
    }
}