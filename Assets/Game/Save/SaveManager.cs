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
        var loader = new SaveHelpers.Loader<SaveData>(loadables, saveData);
        loader.OnComplete += HandleLoadComplete;
        loader.Load();
    }

    private void HandleLoadComplete(SaveHelpers.Loader<SaveData> loader)
    {
        loader.OnComplete -= HandleLoadComplete;
        OnLoadComplete.Invoke();
    }
    
    private void OnApplicationQuit()
    {
        //Save(new SaveInfo("Auto"));
    }
}