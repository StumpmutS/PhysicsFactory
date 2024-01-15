using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

public class KeyCodeIconReference : Singleton<KeyCodeIconReference>
{
    [FormerlySerializedAs("keyCodeDataSo")] [SerializeField] private KeyCodeIconsSO keyCodeIconsSo;
    public KeyCodeIconsSO KeyCodeIconsSo => keyCodeIconsSo;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}