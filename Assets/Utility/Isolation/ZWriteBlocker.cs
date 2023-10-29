using System;
using UnityEngine;
using Utility.Scripts;

public class ZWriteBlocker : MonoBehaviour
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private GameObject blockingPrefab;

    private GameObject _blocker;
    
    private void Awake()
    {
        _blocker = Instantiate(blockingPrefab);
        _blocker.SetActive(false);
    }

    public void Block(int yLevel)
    {
        YLevelConverter.Instance.WorldMinMax(yLevel, yLevel + 1, out var min, out var max);

        var adjustedScale = (mainTransform.rotation * mainTransform.localScale).Abs();
        if (max > mainTransform.position.y + adjustedScale.y / 2 ||
            min < mainTransform.position.y - adjustedScale.y / 2)
        {
            _blocker.SetActive(false);
            return;
        }
        
        _blocker.transform.localScale = new Vector3(adjustedScale.x, max - min, adjustedScale.z) - Vector3.one * .001f;

        _blocker.transform.position =
            new Vector3(mainTransform.position.x, Mathf.Lerp(min, max, .5f), mainTransform.position.z);
        _blocker.SetActive(true);
    }

    public void Unblock()
    {
        _blocker.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_blocker != null) Destroy(_blocker);
    }
}