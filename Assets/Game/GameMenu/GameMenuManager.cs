using System;
using UnityEngine;
using Utility.Scripts;

public class GameMenuManager : Singleton<GameMenuManager>
{
    [SerializeField] private SerializableDictionary<EGameMenuType, Activatable> gameMenus;

    private EGameMenuType _previousMenuType = EGameMenuType.None;
    private Tuple<EGameMenuType, bool> _currentMenuType = new (EGameMenuType.None, false);

    public void ActivateMenu(EGameMenuType menuType)
    {
        if (_currentMenuType.Item2 && menuType != _currentMenuType.Item1)
        {
            _previousMenuType = menuType;
            return;
        }

        _previousMenuType = _currentMenuType.Item1;
        _currentMenuType = new Tuple<EGameMenuType, bool>(menuType, false);
        foreach (var gameMenu in gameMenus.Values)
        {
            gameMenu.TryDeactivate();
        }
        gameMenus[_currentMenuType.Item1].TryActivate();
    }

    public void ActivatePriorityMenu(EGameMenuType menuType)
    {
        _previousMenuType = _currentMenuType.Item1;
        _currentMenuType = new Tuple<EGameMenuType, bool>(menuType, true);
        gameMenus[_previousMenuType].TryDeactivate();
        gameMenus[_currentMenuType.Item1].TryActivate();
    }

    public void DeactivateMenu(EGameMenuType menuType)
    {
        if (menuType != _currentMenuType.Item1) return;

        gameMenus[_currentMenuType.Item1].TryDeactivate();
        gameMenus[_previousMenuType].TryActivate();
        _currentMenuType = new Tuple<EGameMenuType, bool>(_previousMenuType, false);
    }
}