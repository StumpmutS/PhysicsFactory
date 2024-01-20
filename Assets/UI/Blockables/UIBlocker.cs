using UnityEngine;
using Utility.Scripts.Extensions;

public class UIBlocker : MonoBehaviour
{
    private GameObject _blockerObject;
    private IUIBlockable[] _blockables;
    private CanvasGroup _canvasGroup;

    public void Init(UIBlockerData data)
    {
        _blockerObject = Instantiate(data.BlockingPrefab, transform, false);
        
        _blockables = _blockerObject.GetComponents<IUIBlockable>();
        foreach (var blockable in _blockables)
        {
            blockable.Block(data.UIBlockableData);
        }

        _canvasGroup = this.AddOrGetComponent<CanvasGroup>();
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnDestroy()
    {
        foreach (var blockable in _blockables)
        {
            blockable.Unblock();
        }
        
        if (_blockerObject != null) Destroy(_blockerObject);
        //breaks objects that already had a canvas group, will ignore for now
        if (_canvasGroup != null) Destroy(_canvasGroup);
    }
}
