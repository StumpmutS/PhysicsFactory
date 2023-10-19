using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : Building, IEnergySpender
{
    [SerializeField] private EnergySpenderInfo spenderInfo;
    public EnergySpenderInfo SpenderInfo => spenderInfo;
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private SpawnPoint spawnPoint;
    [SerializeField, Tooltip("1.07177 for 1 object per second at 10 charge")] private float spawnRateBase = 1.07177f;
    [SerializeField] private float spawnDelayTime;

    private float _charge;
    private float Charge
    {
        get => _charge;
        set
        {
            _charge = value;
            
            var objectsPerSecond = Mathf.Pow(spawnRateBase, _charge) - 1;
            if (objectsPerSecond <= 0)
            {
                _targetTime = 0;
                return;
            }
            _targetTime = 1 / objectsPerSecond;
        }
    }
    private float _timer;
    private float _targetTime;

    public UnityEvent OnSpawnStart;
    
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_targetTime <= 0)
        {
            _timer = 0;
            return;
        }
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

    public void SetEnergyLevel(float amount)
    {
        var timerPercent = _timer / _targetTime;
        Charge = amount;
        if (!float.IsNaN(timerPercent)) _timer = _targetTime * timerPercent;
    }
}