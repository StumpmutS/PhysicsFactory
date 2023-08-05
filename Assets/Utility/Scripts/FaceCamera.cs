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
            var direction = MainCameraRef.Cam.transform.position - rotator.position;
            rotator.forward = -direction;
        }
    }
}