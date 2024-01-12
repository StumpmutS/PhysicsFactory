using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleInitializer : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    public UnityEvent OnToggleOn = new();
    public UnityEvent OnToggleOff = new();
    
    private void Start()
    {
        if (toggle.isOn) OnToggleOn.Invoke();
        else OnToggleOff.Invoke();
    }
}