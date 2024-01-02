using UnityEngine;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Defaults/LevelOptions")]
public class LevelOptionsSO : ScriptableObject
{
    [SerializeField] private SerializableDictionary<string, string> options;
    public SerializableDictionary<string, string> Options => options;
}