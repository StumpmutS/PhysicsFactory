using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public event Action OnHover = delegate { };
    public event Action OnUnHover = delegate { };
    public event Action OnSelect = delegate { };
    public event Action OnDeselect = delegate { };

    public void Hover()
    {
        OnHover.Invoke();
    }

    public void UnHover()
    {
        OnUnHover.Invoke();
    }
    
    public void Select()
    {
        OnSelect.Invoke();
    }

    public void Deselect()
    {
        OnDeselect.Invoke();
    }
}