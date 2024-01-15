using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AwaitEvent : MonoBehaviour
{
    [SerializeField] private float waitTime;

    private Coroutine _activeCoroutine;
    
    public UnityEvent OnWaitFinished = new();

    public void BeginWait()
    {
        if (_activeCoroutine != null) return;
        
        _activeCoroutine = StartCoroutine(CoWait());
    }

    private IEnumerator CoWait()
    {
        yield return new WaitForSeconds(waitTime);

        OnWaitFinished.Invoke();
    }

    public void StopWait()
    {
        StopCoroutine(_activeCoroutine);
        _activeCoroutine = null;
    }
}