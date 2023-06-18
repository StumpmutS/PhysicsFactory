using System;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
#pragma warning disable CS0108, CS0114
    [SerializeField] private Transform camera;
#pragma warning restore CS0108, CS0114
    [SerializeField] private float moveSpeed;
    [SerializeField] private float levelOffset;

    private bool _movementEnabled = true;
    private Vector2 _moveDirection;
    
    public void SetMoveDirection(Vector2 value)
    {
        _moveDirection = value;
    }

    public void ChangeLevels(int level)
    {
        print(level);
        transform.position = new Vector3(transform.position.x, level + levelOffset, transform.position.z);
    }

    public void Inspect(bool value)
    {
        _movementEnabled = !value;
    }
    
    private void Update()
    {
        if (!_movementEnabled) return;
        
        Move(_moveDirection);
    }

    private void Move(Vector2 value)
    {
        var right = new Vector3(camera.transform.right.x, 0, camera.transform.right.z).normalized;
        var forward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z).normalized;
        transform.Translate(right * value.x * moveSpeed * Time.deltaTime);
        transform.Translate(forward * value.y * moveSpeed * Time.deltaTime);
    }
}