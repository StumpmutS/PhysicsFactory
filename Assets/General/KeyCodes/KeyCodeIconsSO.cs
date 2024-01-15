using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Defaults/Key Code Icons")]
public class KeyCodeIconsSO : ScriptableObject
{
    [SerializeField] private SerializableDictionary<KeyCode, IconData> windowsIcons;
    [SerializeField] private SerializableDictionary<KeyCode, IconData> macOverrides;

    public IconData GetKeyCodeIcon(KeyCode keyCode)
    {
        if (Application.platform == RuntimePlatform.OSXPlayer && macOverrides.TryGetValue(keyCode, out var value))
        {
            return value;
        }

        return windowsIcons[keyCode];
    }
}