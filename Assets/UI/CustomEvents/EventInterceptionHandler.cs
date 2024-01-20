using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Use if child is intercepting events

public class EventInterceptionHandler : MonoBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler, 
    IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IInitializePotentialDragHandler, 
    IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, 
    IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
{
    [SerializeField] private List<GameObject> intercepted;

    public void OnPointerMove(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IPointerMoveHandler>())
            {
                handler.OnPointerMove(eventData);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IPointerEnterHandler>())
            {
                handler.OnPointerEnter(eventData);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IPointerExitHandler>())
            {
                handler.OnPointerExit(eventData);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IPointerDownHandler>())
            {
                handler.OnPointerDown(eventData);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IPointerUpHandler>())
            {
                handler.OnPointerUp(eventData);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IPointerClickHandler>())
            {
                handler.OnPointerClick(eventData);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IBeginDragHandler>())
            {
                handler.OnBeginDrag(eventData);
            }
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IInitializePotentialDragHandler>())
            {
                handler.OnInitializePotentialDrag(eventData);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IDragHandler>())
            {
                handler.OnDrag(eventData);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IEndDragHandler>())
            {
                handler.OnEndDrag(eventData);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IDropHandler>())
            {
                handler.OnDrop(eventData);
            }
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IScrollHandler>())
            {
                handler.OnScroll(eventData);
            }
        }
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IUpdateSelectedHandler>())
            {
                handler.OnUpdateSelected(eventData);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<ISelectHandler>())
            {
                handler.OnSelect(eventData);
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IDeselectHandler>())
            {
                handler.OnDeselect(eventData);
            }
        }
    }

    public void OnMove(AxisEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<IMoveHandler>())
            {
                handler.OnMove(eventData);
            }
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<ISubmitHandler>())
            {
                handler.OnSubmit(eventData);
            }
        }
    }

    public void OnCancel(BaseEventData eventData)
    {
        foreach (var go in intercepted)
        {
            foreach (var handler in go.GetComponents<ICancelHandler>())
            {
                handler.OnCancel(eventData);
            }
        }
    }
}