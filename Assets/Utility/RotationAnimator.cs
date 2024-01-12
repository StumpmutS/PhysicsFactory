using UnityEngine;

public class RotationAnimator : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 rotationAxis = Vector3.forward;
    [SerializeField, Range(-1, 1)] private float defaultDirection = 1;
    [SerializeField] private float speed;

    private float _direction = 1;
    
    private void Update()
    {
        targetTransform.Rotate(rotationAxis, speed * defaultDirection * _direction * Time.deltaTime);
    }
    
    public void SetDirection(float direction)
    {
        _direction = Mathf.Clamp(direction, -1, 1);
    }
}