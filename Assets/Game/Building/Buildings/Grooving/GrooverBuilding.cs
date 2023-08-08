using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GrooverBuilding : Building, IEnergySpender
{
    [SerializeField] private float energyToSpeedMultiplier;
#pragma warning disable CS0108, CS0114
    [SerializeField] private Rigidbody rigidbody;
#pragma warning restore CS0108, CS0114
    [FormerlySerializedAs("info")] [SerializeField] private EnergySpenderInfo spenderInfo;
    public EnergySpenderInfo SpenderInfo => spenderInfo;

    private float _currentSpeed;
    public float CurrentSpeed
    {
        get => _currentSpeed;
        private set
        {
            if (value == _currentSpeed) return;
            _currentSpeed = value;
            OnSpeedChanged.Invoke();
        }
    }

    private float _charge;

    public UnityEvent OnSpeedChanged;

    private void FixedUpdate()
    {
        var pos = rigidbody.position;
        rigidbody.position -= transform.forward * CurrentSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(pos);
    }

    public void SetEnergyLevel(float amount)
    {
        _charge = amount;
        CurrentSpeed = _charge * energyToSpeedMultiplier;
    }
}