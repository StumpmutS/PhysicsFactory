using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts.Extensions;

public class FloatInputController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField, Range(0, 6)] private int decimalPlaces = 2;

    public UnityEvent<float> OnValidValueChanged = new();
    public UnityEvent OnValueInvalid = new();

    private void Awake()
    {
        inputField.onValueChanged.AddListener(HandleInput);
    }

    private void HandleInput(string s)
    {
        if (!float.TryParse(s, out var value))
        {
            OnValueInvalid.Invoke();
            return;
        }
        
        OnValidValueChanged.Invoke(value.Truncate(decimalPlaces));
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(HandleInput);
    }
}