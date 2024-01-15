using UnityEngine;

public class KeyCodeCombinationDisplay : MonoBehaviour
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private IconController iconPrefab;
    [SerializeField] private RectTransform separatorPrefab;
    [SerializeField] private RectTransform stretchedPrefab;
    [SerializeField] private Label descriptionPrefab;
    
    public void Display(KeyCodeCombinationData data)
    {
        layout.Clear();

        for (int i = 0; i < data.KeyCodeCombination.Count; i++)
        {
            var keyCodeCombination = data.KeyCodeCombination[i];
            var keyCodeIcon = KeyCodeIconReference.Instance.KeyCodeIconsSo.GetKeyCodeIcon(keyCodeCombination.KeyCode);
            layout.AddPrefab(iconPrefab, ic => ic.SetIcon(keyCodeIcon));
            if (i >= data.KeyCodeCombination.Count - 1) continue;
            
            layout.AddPrefab(separatorPrefab);
        }

        if (string.IsNullOrEmpty(data.Description)) return;
        
        layout.AddPrefab(stretchedPrefab);
        layout.AddPrefab(descriptionPrefab, l => l.SetLabel(data.Description));
    }
}