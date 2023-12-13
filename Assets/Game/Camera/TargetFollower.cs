using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, speed * Time.deltaTime);
    }
}
