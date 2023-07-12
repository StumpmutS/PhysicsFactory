using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 minStartingForce;
    [SerializeField] private Vector3 maxStartingForce;
    
    public void Spawn(GameObject gameObject)
    {
        var spawned = Instantiate(gameObject, transform.position, transform.rotation);

        if (spawned.TryGetComponent<Rigidbody>(out var rigidbody))
        {

            var right = transform.right * Random.Range(minStartingForce.x, maxStartingForce.x);
            var up = transform.up * Random.Range(minStartingForce.y, maxStartingForce.y);
            var forward = transform.forward * Random.Range(minStartingForce.z, maxStartingForce.z);

            rigidbody.AddForce(right + up + forward, ForceMode.Impulse);
        }
    }
}