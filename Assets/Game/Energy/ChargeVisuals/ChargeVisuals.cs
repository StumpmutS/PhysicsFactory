using TMPro;
using UnityEngine;

public abstract class ChargeVisuals : MonoBehaviour
{
    [SerializeField] private GameObject visualsContainer;
    [SerializeField] private TMP_Text text;
    
    protected abstract string TextValue { get; }
    
    public void Activate()
    {
        visualsContainer.SetActive(true);
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = TextValue;
    }
    
    public void Deactivate()
    {
        visualsContainer.SetActive(false);
    }
}