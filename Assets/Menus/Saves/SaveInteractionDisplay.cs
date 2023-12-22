using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SaveInteractionDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text saveTypeText;
    [SerializeField] private TMP_Text dateTimeText;

    private SaveDisplayData _data;

    public UnityEvent<SaveDisplayData> OnLoadRequest = new();
    
    public void Init(SaveDisplayData data)
    {
        _data = data;
        nameText.text = data.SaveData.SaveInfo.Name;
        saveTypeText.text = data.SaveData.SaveInfo.SaveType;
        dateTimeText.text = data.SaveData.SaveInfo.DateTime.DateTime.ToString(CultureInfo.InvariantCulture);
    }

    public void RequestLoad()
    {
        OnLoadRequest.Invoke(_data);
    }
}