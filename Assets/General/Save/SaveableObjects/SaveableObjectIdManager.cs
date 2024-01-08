using System.Collections.Generic;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class SaveableObjectIdManager : Singleton<SaveableObjectIdManager>
{
    private HashSet<SaveableObject> _objects = new();
    private List<SaveableObject> _indexedObjects = new();

    public int AddObject(SaveableObject obj)
    {
        if (!_objects.Add(obj)) return -1;

        for (int i = 0; i < _indexedObjects.Count; i++)
        {
            if (_indexedObjects[i] != null) continue;
            
            _indexedObjects[i] = obj;
            return i;
        }
        
        _indexedObjects.Add(obj);
        return _indexedObjects.Count - 1;
    }

    public void IdentifyObject(SaveableObject obj, int id)
    {
        if (_indexedObjects.Count <= id) _indexedObjects.Equalize(id + 1);
        _indexedObjects[id] = obj;
    }
    
    public void RemoveObject(SaveableObject saveableObject)
    {
        _objects.Remove(saveableObject);
        _indexedObjects.Remove(saveableObject);
    }

    public bool TryGet(int id, out SaveableObject obj)
    {
        if (id > -1 && id < _indexedObjects.Count)
        {
            obj = _indexedObjects[id];
            return true;
        }

        obj = null;
        return false;
    }
}