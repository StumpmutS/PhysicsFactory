using UnityEngine;

public class StickRigidbodyAffector : RigidbodyAffector
{
    [SerializeField] private float energyToForceMultiplier;
    
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
        var contact = collision.GetContact(0);
        collision.rigidbody.AddForceAtPosition(contact.normal * energyToForceMultiplier, contact.point);
    }
}
