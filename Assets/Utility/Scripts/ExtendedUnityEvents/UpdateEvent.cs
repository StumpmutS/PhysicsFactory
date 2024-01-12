using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utility.Scripts
{
    public class UpdateEvent : MonoBehaviour
    {
        public UnityEvent OnUpdate = new();
        
        private void Update()
        {
            OnUpdate.Invoke();
        }
    }
}