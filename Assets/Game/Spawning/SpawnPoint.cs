using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void Spawn<T>(T obj, Action<T> initCallback = null) where T : Component
    {
        var instantiated = Instantiate(obj, transform.position, transform.rotation);
        initCallback?.Invoke(instantiated);
    }
    
    public void Spawn(GameObject obj, Action<GameObject> initCallback = null)
    {
        var instantiated = Instantiate(obj, transform.position, transform.rotation);
        initCallback?.Invoke(instantiated);
    }
}
