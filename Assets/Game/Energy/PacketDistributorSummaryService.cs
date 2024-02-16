using System;
using UnityEngine;

public class PacketDistributorSummaryService : DataService<string>
{
    [SerializeField] private ChargePacketDistributor packetDistributor;
    [SerializeField] [TextArea] [Tooltip("Option 0: Charge Used, Option 1: Max Charge")]
    private string summary = "Charge Used: {0} / {1}";

    private void Awake()
    {
        packetDistributor.OnChargeUpdate.AddListener(HandleDistributorUpdate);
    }

    public override string RequestData()
    {
        return string.Format(summary, 
            packetDistributor.CurrentCharge.ToString("F2"),
            packetDistributor.MaxCharge.ToString("F2"));
    }

    private void HandleDistributorUpdate()
    {
        OnUpdated.Invoke(RequestData());
    }
}