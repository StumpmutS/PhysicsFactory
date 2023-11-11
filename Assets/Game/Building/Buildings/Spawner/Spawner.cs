using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private SpawnPoint spawnPoint;
    [SerializeField] private float spawnDelayTime;

    private float _timer;
    private float _targetTime;

    public UnityEvent OnSpawnStart;
    
    private void Update()
    {
        if (_targetTime <= 0)
        {
            _timer = 0;
            return;
        }
        
        _timer += Time.deltaTime;
        for (float i = _targetTime; i <= _timer; i += _targetTime)
        {
            StartCoroutine(BeginSpawn());
        }
        _timer %= _targetTime;
    }

    private IEnumerator BeginSpawn()
    {
        OnSpawnStart.Invoke();
        yield return new WaitForSeconds(spawnDelayTime);
        spawnPoint.Spawn(spawnedObject);
    }

    public void SetTargetTime(float amount)
    {
        var timerPercent = _timer / _targetTime;
        _targetTime = amount;
        if (!float.IsNaN(timerPercent)) _timer = _targetTime * timerPercent;
    }
}