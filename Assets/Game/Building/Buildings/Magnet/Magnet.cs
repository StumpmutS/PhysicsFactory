using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float energyToForceMultiplier;
    [SerializeField] private float maxInfluenceDistance;
    [SerializeField] private LayerMask dolboidLayer;
    
    private void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(transform.position, maxInfluenceDistance, dolboidLayer);
        foreach (var collider in colliders)
        {
            if (collider.attachedRigidbody == null) continue;

            var direction = transform.position - collider.transform.position;
            
            collider.attachedRigidbody.AddForce(direction * energyToForceMultiplier / direction.sqrMagnitude);
        }
    }
}
