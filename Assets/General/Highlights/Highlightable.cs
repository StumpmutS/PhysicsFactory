using UnityEngine;
using Utility.Scripts;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private LayerMask highlightLayer;

    private LayerMask _defaultLayer;
    
    private void Awake()
    {
        _defaultLayer = target.layer;
    }

    private void Start()
    {
        HighlightManager.Instance.AddHighlightable(this);
    }

    public void ActivateHighlight()
    {
        target.layer = LayerHelper.MaskToLayer(highlightLayer);
    }

    public void DeactivateHighlight()
    {
        target.layer = LayerHelper.MaskToLayer(_defaultLayer);
    }

    private void OnDestroy()
    {
        HighlightManager.Instance.RemoveHighlightable(this);
    }
}