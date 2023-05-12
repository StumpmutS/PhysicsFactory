using UnityEngine;

public class StickRigidbodyAffector : RigidbodyAffector
{
    public override void AffectRigidbody(Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public override void UnaffectRigidbody(Rigidbody rigidbody)
    {
        rigidbody.constraints = RigidbodyConstraints.None;
    }
}