using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility.Scripts;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private string saveDirectoryName = "Saves";
    [SerializeField] private string defaultLocalSaveFileName;
    
    private LocalSaveHandler _localSaveHandler;

    public UnityEvent<SaveData> OnStartSave = new();
    public UnityEvent<SaveData> OnStartLoad = new();
    public UnityEvent OnLoadComplete = new();

    protected override void Awake()
    {
        base.Awake();
        _localSaveHandler = new LocalSaveHandler(defaultLocalSaveFileName,
            Path.Combine(Application.persistentDataPath, saveDirectoryName));
    }

    public void Save(SaveInfo info)
    {
        var saveData = new SaveData(info);

        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<SaveData>(), saveData);
        
        _localSaveHandler.Save(saveData);
    }

    public void Load()
    {
        var saveData = _localSaveHandler.Load();
        var loadables = SaveHelpers.GetLoadables<SaveData>();
        var loader = new UnorderedLoader(loadables.Select(l => new LoadableData(() => l.Load(saveData))));
        loader.Load().OnComplete += HandleLoadComplete;
    }

    private void HandleLoadComplete(LoadingInfo _)
    {
        OnLoadComplete.Invoke();
    }
    
    private void OnApplicationQuit()
    {
        //Save(new SaveInfo("Auto"));
    }
}