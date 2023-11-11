using System.Collections.Generic;
using Utility.Scripts;

public class IsolationManager : Singleton<IsolationManager>
{
    private HashSet<Isolatable> _isolatables = new();
    private bool _isolated;
    private int _isolationLevel;

    private void Start()
    {
        YLevelManager.Instance.OnYLevelChanged.AddListener(HandleYLevelChanged);
    }

    public void AddIsolatable(Isolatable isolatable)
    {
        _isolatables.Add(isolatable);
        if (_isolated) IsolateIsolatable(isolatable, _isolationLevel);
    }

    public void RemoveIsolatable(Isolatable isolatable)
    {
        _isolatables.Remove(isolatable);
    }

    public void Isolate(int yLevel = -1)
    {
        _isolated = true;
        
        if (yLevel < 0) yLevel = YLevelManager.Instance.YLevel;
        _isolationLevel = yLevel;
        
        foreach (var isolatable in _isolatables)
        {
            IsolateIsolatable(isolatable, yLevel);
        }
    }

    private void IsolateIsolatable(Isolatable isolatable, int yLevel) => isolatable.Isolate(yLevel);

    public void DeIsolate()
    {
        _isolated = false;
        
        foreach (var isolatable in _isolatables)
        {
            isolatable.DeIsolate();
        }
    }

    private void HandleYLevelChanged(int arg0)
    {
        if (_isolated) Isolate(YLevelManager.Instance.YLevel);
    }
}