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
        HandleToggle(toggle.isOn);
    }

    protected abstract void HandleToggle(bool value);
}