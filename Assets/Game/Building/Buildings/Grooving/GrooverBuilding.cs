using UnityEngine;
using UnityEngine.Serialization;

public class GrooverBuilding : Building, IEnergySpender
{
    [SerializeField] private float energyToSpeedMultiplier;
#pragma warning disable CS0108, CS0114
    [SerializeField] private Rigidbody rigidbody;
#pragma warning restore CS0108, CS0114
    [FormerlySerializedAs("info")] [SerializeField] private EnergySpenderInfo spenderInfo;
    public EnergySpenderInfo SpenderInfo => spenderInfo;

    private float _charge;
    private float _currentSpeed;

    private void FixedUpdate()
    {
        var pos = rigidbody.position;
        rigidbody.position -= transform.forward * _currentSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(pos);
    }

    public void SetEnergyLevel(float amount)
    {
        _charge = amount;
        _currentSpeed = _charge * energyToSpeedMultiplier;
    }
}