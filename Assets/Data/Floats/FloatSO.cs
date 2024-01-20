using UnityEngine;

[CreateAssetMenu(menuName = "Defaults/Floats")]
public class FloatSO : ScriptableObject
{
    [SerializeField] private float value;
    public float Value => value;
}