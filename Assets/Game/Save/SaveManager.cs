using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private string localSaveFileName;
    
    private LocalSaveHandler _localSaveHandler;
    private HashSet<ILoadable> _completedLoadables = new();
    private int _loadTarget;

    public UnityEvent<SaveData> OnSave = new();
    public UnityEvent<SaveData> OnLoad = new();
    public UnityEvent OnLoadComplete = new();

    protected override void Awake()
    {
        base.Awake();
        _localSaveHandler = new LocalSaveHandler(localSaveFileName, Application.persistentDataPath);
    }

    public void Save()
    {
        var saveData = new SaveData();
        
        foreach (var saveable in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>())
        {
            saveable.Save(saveData);
        }
        
        _localSaveHandler.Save(saveData);
    }

    public void Load()
    {
        var saveData = _localSaveHandler.Load();
        var loadables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ILoadable>().ToHashSet();
        _completedLoadables.Clear();
        _loadTarget = loadables.Count;
        
        foreach (var loadable in loadables)
        {
            loadable.OnLoadComplete += HandleLoadComplete;
            loadable.Load(saveData);
        }
    }

    private void HandleLoadComplete(ILoadable loadable)
    {
        loadable.OnLoadComplete -= HandleLoadComplete;
        _completedLoadables.Add(loadable);
        if (_completedLoadables.Count == _loadTarget)
        {
            OnLoadComplete.Invoke();
        }
    }
    
    private void OnApplicationQuit()
    {
        Save();
    }
}