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

    private void GenerateInwardForce(Collision collision)
    {
        var contacts = new List<ContactPoint>();
        collision.GetContacts(contacts);
        foreach (var contact in contacts)
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