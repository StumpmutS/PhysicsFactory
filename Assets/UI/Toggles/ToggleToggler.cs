using UnityEngine;
using UnityEngine.UI;

public class ToggleToggler : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    public void Toggle()
    {
        toggle.isOn = !toggle.isOn;
    }
}
