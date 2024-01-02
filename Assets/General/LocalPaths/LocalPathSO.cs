using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Defaults/LocalPathData")]
public class LocalPathSO : ScriptableObject
{
    [FormerlySerializedAs("localSavePathInfo")] [SerializeField] private LocalPathData localPathData;
    public LocalPathData LocalPathData => localPathData;
}