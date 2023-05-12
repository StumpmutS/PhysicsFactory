using UnityEngine;

public class RigidbodyAffector : MonoBehaviour
{
    public virtual void AffectRigidbody(Rigidbody rigidbody) { }
    public virtual void ContinuouslyAffectRigidbody(Rigidbody rigidbody) { }
    public virtual void UnaffectRigidbody(Rigidbody rigidbody) { }
}