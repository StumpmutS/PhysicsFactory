using UnityEngine;

[CreateAssetMenu(menuName = "Time/DeltaTimeReference")]
public class DeltaTimeReference : ScriptableObject
{
    [SerializeField] private ETimeType timeType;

    public float DeltaTime
    {
        get
        {
            return timeType switch
            {
                ETimeType.Delta => Time.deltaTime,
                ETimeType.UnscaledDelta => Time.unscaledDeltaTime,
                ETimeType.Fixed => Time.fixedTime,
                ETimeType.UnscaledFixed => Time.fixedUnscaledTime,
                _ => 0f
            };
        }
    }
}