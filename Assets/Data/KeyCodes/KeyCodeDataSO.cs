using UnityEngine;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Defaults/Key Code Data")]
public class KeyCodeDataSO : ScriptableObject
{
    [SerializeField] private SerializableDictionary<KeyCode, KeyCodeData> keyCodeData;
}