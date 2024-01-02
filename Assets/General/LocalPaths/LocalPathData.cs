using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class LocalPathData
{
    [FormerlySerializedAs("directoryName")] [FormerlySerializedAs("saveDirectoryName")] [SerializeField] private string directoryPath;
    public string DirectoryPath => directoryPath;
    [FormerlySerializedAs("defaultLocalSaveFileName")] [SerializeField] private string defaultLocalFileName;
    public string DefaultLocalFileName => defaultLocalFileName;
}