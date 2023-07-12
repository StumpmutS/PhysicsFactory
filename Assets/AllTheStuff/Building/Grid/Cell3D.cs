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
        selectable.OnHover.AddListener(HandleHovered);
        selectable.OnHoverStop.AddListener(HandleUnHovered);
        selectable.OnSelect.AddListener(HandleSelected);
        selectable.OnDeselect.AddListener(HandleDeselected);
    }

    public void Init(Cell3DInfo info)
    {
        Info = info;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(Info.Center.x, Info.Center.y, Info.Center.z);
    }

    private void HandleHovered(Selectable _)
    {
        OnHover.Invoke(this);
    }

    private void HandleUnHovered(Selectable _)
    {
        OnUnHover.Invoke(this);
    }

    private void HandleSelected(Selectable _)
    {
        OnSelect.Invoke(this);
    }

    private void HandleDeselected(Selectable _)
    {
        OnDeselect.Invoke(this);
    }
}