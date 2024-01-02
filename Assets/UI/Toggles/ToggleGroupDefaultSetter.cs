using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupDefaultSetter : MonoBehaviour
{
    [SerializeField] private Toggle defaultToggle;

    private void Start()
    {
        defaultToggle.isOn = false;
        defaultToggle.isOn = true;
    }
}