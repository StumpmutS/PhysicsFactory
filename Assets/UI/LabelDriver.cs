using TMPro;
using UnityEngine;

public class LabelDriver : MonoBehaviour
{
    [SerializeField] private Label label;
    [SerializeField] private DataService<string> textService;

    private void Awake()
    {
        if (label == null) label = GetComponent<Label>();
        if (textService == null) textService = GetComponent<DataService<string>>();
        
        textService.OnUpdated.AddListener(SetText);
    }

    private void Start()
    {
        SetText(textService.RequestData());
    }

    private void SetText(string s)
    {
        label.SetLabel(s);
    }

    private void OnDestroy()
    {
        if (textService != null) textService.OnUpdated.RemoveListener(SetText);
    }
}