using TMPro;
using UnityEngine;

public class SaveDisplayToggle : MonoBehaviour
{
    [SerializeField] private CallbackToggle callbackToggle;
    [SerializeField] private TMP_Text text;

    public void Init(SaveDisplayData saveDisplayData, CallbackToggleData callbackToggleData)
    {
        callbackToggle.Init(callbackToggleData);
        var info = saveDisplayData.SaveData.SaveInfo;
        text.text = $"{info.Name} - {info.DateTime.DateTime}";
    }
}