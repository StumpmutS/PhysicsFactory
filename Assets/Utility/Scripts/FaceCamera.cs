using System;
using UnityEngine;

namespace Utility.Scripts
{
    public class FaceCamera : MonoBehaviour
    {
        [SerializeField] private Transform rotator;

        private void Awake()
        {
            if (rotator == null) rotator = transform;
        }

        private void LateUpdate()
        {
            rotator.LookAt(MainCameraRef.Cam.transform);
        }
    }
}