using UnityEngine;

[CreateAssetMenu(menuName = "Modification")]
public class ModificationData : ScriptableObject
{
    [SerializeField] private ModificationInfo info;
    public ModificationInfo Info => info;
}