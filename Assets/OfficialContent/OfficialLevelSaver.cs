using System.IO;
using UnityEditor;
using UnityEngine;

public class OfficialLevelSaver : MonoBehaviour
{
    [SerializeField] private LocalPathSO pathSo;

    private bool _editing;
    
    public void HandleEditMode()
    {
        _editing = true;
    }

    #if UNITY_EDITOR
    private void OnGUI()
    {
        if (!_editing) return;
        
        if (GUI.Button(new Rect(10, 200, 150, 50), "Save Official"))
        {
            SaveToAsset();
        }
    }

    private void SaveToAsset()
    {
        var asset = ScriptableObject.CreateInstance<LevelSO>();
        var levelData = LevelSaveHelpers.GetCurrentLevelData();
        asset.LevelData = levelData;
        
        var path = Path.Combine(pathSo.LocalPathData.DirectoryPath, $"{levelData.LevelInfo.Name}.asset");
        
        AssetDatabase.CreateAsset(asset, path);
        
        Debug.Log($"Official level asset saved to {path}");
    }
    #endif
}
