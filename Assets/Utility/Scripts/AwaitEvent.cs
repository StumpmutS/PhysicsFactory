using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utility.Scripts
{
    public class AwaitEvent : MonoBehaviour
    {
        [SerializeField] private float waitTime;

        private Coroutine _activeCoroutine;
    
        public UnityEvent OnWaitFinished = new();

        public void Init(float waitTime)
        {
            this.waitTime = waitTime;
        }
        
        public void BeginWait()
        {
            if (_activeCoroutine != null) return;
            
            _activeCoroutine = StartCoroutine(CoWait());
        }

        public void StopWait()
        {
            if (_activeCoroutine == null) return;
            
            StopCoroutine(_activeCoroutine);
            _activeCoroutine = null;
        }

        public void ResetOrBeginWait()
        {
            StopWait();
            BeginWait();
        }

        private IEnumerator CoWait()
        {
            yield return new WaitForSeconds(waitTime);
            
            OnWaitFinished.Invoke();
        }
    }
}