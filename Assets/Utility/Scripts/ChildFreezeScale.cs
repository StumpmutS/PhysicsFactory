using System;
using UnityEngine;

namespace Utility.Scripts
{
    public class ChildFreezeScale : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        
        private Vector3 _desiredScale;

        private void Awake()
        {
            _desiredScale = transform.localScale;
        }

        private void LateUpdate()
        {
            transform.localScale = new Vector3(_desiredScale.x / parent.localScale.x,
                _desiredScale.y / parent.localScale.y, _desiredScale.z / parent.localScale.z);
        }
    }
}