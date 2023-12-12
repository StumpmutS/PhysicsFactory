using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class LayoutDisplay : MonoBehaviour
{
    [SerializeField] private LayoutGroup layoutGroup;
    
    public HashSet<RectTransform> Children { get; private set; } = new();

    public UnityEvent<RectTransform> OnAdd = new();
    public UnityEvent<RectTransform> OnRemove = new();
    
    private void Awake()
    {
        if (layoutGroup == null) layoutGroup = GetComponent<LayoutGroup>();
    }

    public void Add(RectTransform rectTransform)
    {
        if (!Children.Add(rectTransform)) return;
        
        rectTransform.AddOrGetComponent<DestroyedListener>().OnDestroyed.AddListener(HandleChildDestroyed);
        rectTransform.SetParent(layoutGroup.transform, false);
        OnAdd.Invoke(rectTransform);
    }

    private void HandleChildDestroyed(DestroyedListener destroyedListener)
    {
        destroyedListener.OnDestroyed.RemoveListener(HandleChildDestroyed);
        if (destroyedListener.transform is not RectTransform rectTransform || !Children.Remove(rectTransform)) return;

        OnRemove.Invoke(rectTransform);
    }

    public void Clear()
    {
        var childrenCopy = Children.ToHashSet();
        foreach (var child in childrenCopy)
        {
            Destroy(child.gameObject);
        }
        
        Children.Clear();
    }
}