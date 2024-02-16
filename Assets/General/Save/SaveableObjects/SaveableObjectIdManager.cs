using System.Collections.Generic;
using Utility.Scripts;


public class SaveableObjectIdManager : Singleton<SaveableObjectIdManager>
{ 
    public const int SceneIdIndex = 1024;

    private SerializableDictionary<int, SaveableObject> _idObjects = new();
    private HashSet<SaveableObject> _uniqueObjects = new();
    private int idIndex;

    protected override void Awake()
    {
        base.Awake();
        idIndex = SceneIdIndex;
    }

    public int AddObject(SaveableObject obj)
    {
        if (!_uniqueObjects.Add(obj)) return -1;

        while (_idObjects.ContainsKey(idIndex)) idIndex++;
        
        _idObjects[idIndex] = obj;
        idIndex++;
        
        return idIndex - 1;
    }

    public void IdentifyObject(SaveableObject obj, int id)
    {
        if (!_uniqueObjects.Add(obj)) return;
        
        _idObjects[id] = obj;
    }
    
    public void RemoveObject(SaveableObject saveableObject)
    {
        if (!_uniqueObjects.Remove(saveableObject)) return;
        
        _idObjects.Remove(saveableObject.Id);
    }

    public bool TryGet(int id, out SaveableObject obj)
    {
        return _idObjects.TryGetValue(id, out obj);
    }

    public bool TryGetSceneObject(int id, out SaveableObject obj)
    {
        obj = null;
        return id >= 0 && id < SceneIdIndex && TryGet(id, out obj);
    }
}
