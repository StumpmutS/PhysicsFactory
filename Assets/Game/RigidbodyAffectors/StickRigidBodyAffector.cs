using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StickRigidBodyAffector : RigidBodyAffector, IEnergySpender
{
    [SerializeField] private EnergySpenderInfo spenderInfo;
    public EnergySpenderInfo SpenderInfo => spenderInfo;
    [SerializeField] private float energyToForceMultiplier;

    private float _charge;

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
                (contact.normal * energyToForceMultiplier * _charge, contact.point);
        }
    }

    public void SetEnergyLevel(float amount)
    {
        _charge = amount;
    }
}