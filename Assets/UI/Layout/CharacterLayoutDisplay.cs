using UnityEngine;

public class CharacterLayoutDisplay : MonoBehaviour
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private float startingFontSize = 40;

    private float _fontSize;
    public float FontSize
    {
        get => _fontSize;
        set
        {
            if (value == _fontSize) return;
            
            UpdateFont(value);
        }
    }

    private void Start()
    {
        layout.OnAdd.AddListener(_ => UpdateFont(FontSize));
        layout.OnRemove.AddListener(_ => UpdateFont(FontSize));
        FontSize = startingFontSize;
    }

    private void UpdateFont(float value)
    {
        _fontSize = value;

        foreach (var child in layout.Children)
        {
            if (child.TryGetComponent<TextCharacter>(out var character))
            {
                character.SetFontSize(FontSize);
            }
        }
    }
}
