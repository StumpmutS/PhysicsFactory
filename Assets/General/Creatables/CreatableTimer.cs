using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Creatable))]
public class CreatableTimer : MonoBehaviour
{
    [SerializeField] private float disposeTime;
    
    private Creatable _creatable;

    private void Awake()
    {
        _creatable = GetComponent<Creatable>();
    }

    private void Start()
    {
        StartCoroutine(CoTimer());
    }

    private IEnumerator CoTimer()
    {
        yield return new WaitForSeconds(disposeTime);
        
        if (_creatable != null) _creatable.Dispose();
    }
}