using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ConveyorBuilding : Building
{
#pragma warning disable CS0108, CS0114
    [SerializeField] private Rigidbody rigidbody;
#pragma warning restore CS0108, CS0114

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

    public UnityEvent OnSpeedChanged;

    public void SetSpeed(float amount)
    {
        CurrentSpeed = amount;
    }

    private void FixedUpdate()
    {
        var pos = rigidbody.position;
        rigidbody.position -= transform.forward * CurrentSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(pos);
    }
}