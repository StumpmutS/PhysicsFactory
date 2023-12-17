using System;
using UnityEngine;

namespace Utility.Scripts
{
    [Serializable]
    public class TransformData
    {
        public Vector3 WorldPosition;
        public Quaternion WorldRotation;
        public Vector3 LocalScale;

        public TransformData(Vector3 worldPosition, Quaternion worldRotation, Vector3 localScale)
        {
            WorldPosition = worldPosition;
            WorldRotation = worldRotation;
            LocalScale = localScale;
        }

        public TransformData(Transform transform) : this(transform.position, transform.rotation, transform.localScale) { }
    }
}