using TMPro;
using UnityEngine;

public class TextDriver : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private DataService<string> textService;

    private void Awake()
    {
        textService.OnUpdated.AddListener(SetText);
    }

    private void Start()
    {
        SetText(textService.RequestData());
    }

    private void SetText(string s)
    {
        text.text = s;
    }

    private void OnDestroy()
    {
        if (textService != null) textService.OnUpdated.RemoveListener(SetText);
    }
}