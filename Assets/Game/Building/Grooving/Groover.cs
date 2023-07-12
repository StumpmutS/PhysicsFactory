using UnityEngine;

public class Groover : MonoBehaviour, IEnergySpender
{
    [SerializeField] private float energyToSpeedMultiplier;
#pragma warning disable CS0108, CS0114
    [SerializeField] private Rigidbody rigidbody;
#pragma warning restore CS0108, CS0114
    [SerializeField] private EnergySpenderInfo info;
    public EnergySpenderInfo Info => info;

    private int _charge;
    private float _currentSpeed;

    private void FixedUpdate()
    {
        var pos = rigidbody.position;
        rigidbody.position -= transform.forward * _currentSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(pos);
    }

    public void SetEnergyLevel(int amount)
    {
        _charge = amount;
        _currentSpeed = _charge * energyToSpeedMultiplier;
    }
}