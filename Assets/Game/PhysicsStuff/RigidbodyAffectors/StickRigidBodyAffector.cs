using System;
using System.Collections.Generic;
using UnityEngine;

public class StickRigidBodyAffector : RigidBodyAffector, IChargeable
{
    [SerializeField] private DataService<ContextData> contextService;
    public ContextData Context => contextService.RequestData();
    [SerializeField] private float energyToForceMultiplier;

    public ChargePacket ChargePacket { get; set; }

    private EnergySpreadController _controller;

    public override void Init(RigidBodyAffectorContainer rigidBodyAffectorContainer)
    {
        if (!rigidBodyAffectorContainer.TryGetComponent<EnergySpreadController>(out _controller)) return;
        
        _controller.RegisterSpender(this);
    }

    public override void AffectRigidbody(Collision collision)
    {
        GenerateInwardForce(collision);
    }

    public override void ContinuouslyAffectRigidbody(Collision collision)
    {
        GenerateInwardForce(collision);
    }

    private readonly List<ContactPoint> _contactPoints = new();

    private void GenerateInwardForce(Collision collision)
    {
        collision.GetContacts(_contactPoints);
        foreach (var contact in _contactPoints)
        {
            collision.rigidbody.AddForceAtPosition
                (contact.normal * energyToForceMultiplier * ChargePacket.CurrentCharge.AsFloat(), contact.point);
        }
    }

    private void OnDestroy()
    {
        if (_controller != null) _controller.DeregisterSpender(this);
    }
}