using UnityEngine;
using UnityEngine.Events;

namespace Utility.Scripts
{
    public class StartScaleZEvent : MonoBehaviour
    {
        public UnityEvent<float> OnStart = new();
        
        private void Start()
        {
            OnStart.Invoke(transform.localScale.z);
        }
    }
}