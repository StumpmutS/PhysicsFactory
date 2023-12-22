using System;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Defaults/LocalSavePathData")]
public class LocalSavePathSO : ScriptableObject
{
    [SerializeField] private LocalSavePathInfo localSavePathInfo;
    public LocalSavePathInfo LocalSavePathInfo => localSavePathInfo;
}

[Serializable]
public class LocalSavePathInfo
{
    [SerializeField] private string saveDirectoryName = "Saves";
    public string SaveDirectoryPath => Path.Combine(Application.persistentDataPath, saveDirectoryName);
    [SerializeField] private string defaultLocalSaveFileName;
    public string DefaultLocalSaveFileName => defaultLocalSaveFileName;
}