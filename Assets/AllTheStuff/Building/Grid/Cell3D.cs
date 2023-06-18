using System;
using UnityEngine;

public class Cell3D : MonoBehaviour
{
    [SerializeField] private Selectable selectable;
    public Selectable Selectable => selectable;
    
    public Cell3DInfo Info { get; private set; }
    
    public event Action<Cell3D> OnHover = delegate { };
    public event Action<Cell3D> OnUnHover = delegate { };
    public event Action<Cell3D> OnSelect = delegate { };
    public event Action<Cell3D> OnDeselect = delegate { };


    private void Awake()
    {
        selectable.OnHover += HandleHovered;
        selectable.OnUnHover += HandleUnHovered;
        selectable.OnSelect += HandleSelected;
        selectable.OnDeselect += HandleDeselected;
    }

    public void Init(Cell3DInfo info)
    {
        Info = info;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(Info.Center.x, Info.Center.y - (float) Info.Size / 2, Info.Center.z);
    }

    private void HandleHovered()
    {
        OnHover.Invoke(this);
    }

    private void HandleUnHovered()
    {
        OnUnHover.Invoke(this);
    }

    private void HandleSelected()
    {
        OnSelect.Invoke(this);
    }

    private void HandleDeselected()
    {
        OnDeselect.Invoke(this);
    }
}