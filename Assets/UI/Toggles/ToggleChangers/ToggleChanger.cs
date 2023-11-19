using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleChanger : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleToggle);
    }

    private void Start()
    {
        ChangeValue(toggle.isOn);
    }

    private void HandleToggle(bool value)
    {
        ChangeValue(toggle.isOn);
    }
    
    protected abstract void ChangeValue(bool value);
}