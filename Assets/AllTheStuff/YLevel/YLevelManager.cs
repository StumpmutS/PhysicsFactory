using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class YLevelManager : Singleton<YLevelManager>
{
    [SerializeField] private int minLevel, maxLevel;
    
    private int _yLevel;
    public int YLevel
    {
        get => _yLevel;
        private set
        {
            if (_yLevel == value) return;
            
            _yLevel = Mathf.Clamp(value, minLevel, maxLevel);
            OnYLevelChanged.Invoke(_yLevel);
        }
    }

    public UnityEvent<int> OnYLevelChanged;

    public void ChangeLevels(float scrollValue)
    {
        YLevel -= Mathf.RoundToInt(scrollValue / 120);
    }
}