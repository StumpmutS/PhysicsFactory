using UnityEngine;

public class RigidBodyAffector : MonoBehaviour
{
    public virtual void AffectRigidbody(Collision collision) { }
    public virtual void ContinuouslyAffectRigidbody(Collision collision) { }
    public virtual void UnaffectRigidbody(Collision collision) { }
}