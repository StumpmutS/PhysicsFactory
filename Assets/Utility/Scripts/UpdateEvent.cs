using UnityEngine;
using UnityEngine.Events;

namespace Utility.Scripts
{
    public class UpdateEvent : MonoBehaviour
    {
        public UnityEvent OnUpdate;
        
        private void Update()
        {
            OnUpdate.Invoke();
        }
    }
}