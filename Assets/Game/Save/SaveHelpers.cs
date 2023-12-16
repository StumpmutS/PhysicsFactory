using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveHelpers
{
    public static IEnumerable<ISaveable<T>> GetSaveables<T>()
    {
        return UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable<T>>();
    }
    
    public static IEnumerable<ILoadable<T>> GetLoadables<T>()
    {
        return UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ILoadable<T>>();
    }
    
    public static void GroupSave<T>(IEnumerable<ISaveable<T>> saveables, T data)
    {
        foreach (var saveable in saveables)
        {
            saveable.Save(data);
        }
    }

    public class Loader<TData>
    {
        private HashSet<ILoadable<TData>> _loadables;
        private TData _data;
        private HashSet<ILoadable<TData>> _loaded = new();

        public event Action<Loader<TData>> OnComplete = delegate { };

        public Loader(IEnumerable<ILoadable<TData>> loadables, TData data)
        {
            _loadables = loadables.ToHashSet();
            _data = data;
        }

        public void Load()
        {
            _loaded.Clear();
            
            foreach (var loadable in _loadables)
            {
                loadable.OnLoadComplete += HandleLoadComplete;
                loadable.Load(_data);
            }
        }

        private void HandleLoadComplete(ILoadable<TData> loadable)
        {
            _loaded.Add(loadable);
            if (_loaded.Count == _loadables.Count) OnComplete.Invoke(this);
        }
    }
}