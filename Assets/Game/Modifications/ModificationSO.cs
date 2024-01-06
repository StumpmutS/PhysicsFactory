using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Modification")]
public class ModificationSO : ScriptableObject
{
    [FormerlySerializedAs("info")] [SerializeField] private ModificationData data;
    public ModificationData Data => data;
}