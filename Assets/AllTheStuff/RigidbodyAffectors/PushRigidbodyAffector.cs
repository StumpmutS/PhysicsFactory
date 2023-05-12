using UnityEngine;

public class PushRigidbodyAffector : RigidbodyAffector
{
    [SerializeField] private Transform referenceTransform;
    [SerializeField] private float pushForce;
    
    public override void ContinuouslyAffectRigidbody(Rigidbody rigidbody)
    {
        rigidbody.AddForce(referenceTransform.forward * pushForce);
    }
}