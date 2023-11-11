using System;
using UnityEngine;

public class Dolboid : MonoBehaviour
{
    [SerializeField] private DolboidInfo startingInfo;

    public DolboidInfo CurrentInfo { get; private set; }

    public event Action<DolboidInfo> OnDolboidChanged = delegate {  };

    private void Awake()
    {
        SetDolboidInfo(startingInfo);
    }

    public void SetDolboidInfo(DolboidInfo info)
    {
        CurrentInfo = info;
        OnDolboidChanged.Invoke(info);
    }
}
