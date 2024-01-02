using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private LevelData levelData;
    public LevelData LevelData => levelData;
}