using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private SpawnPoint spawnPoint;
    [SerializeField] private float spawnRate;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer < 1 / spawnRate) return;
        
        spawnPoint.Spawn(spawnedObject);
        _timer = 0;
    }
}