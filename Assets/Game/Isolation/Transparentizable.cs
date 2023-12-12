using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class Transparentizable : MonoBehaviour
{
    [SerializeField] protected Transform mainTransform;
    [SerializeField] protected MaterialManager materialManager;
    [SerializeField] private float epsilon = .0001f;
    protected virtual float Epsilon => epsilon;

        private static readonly int MinYUntransparentized = Shader.PropertyToID("_MinYUntransparentized");
    private static readonly int MaxYUntransparentized = Shader.PropertyToID("_MaxYUntransparentized");

    public void Transparentize(int yLevel)
    {
        YLevelConverter.Instance.WorldMinMax(yLevel, yLevel + 1, out var min, out var max);
        var localEpsilon = Epsilon;
        var adjustedScale = (mainTransform.rotation * mainTransform.localScale).Abs();
        if (mainTransform.position.y + adjustedScale.y / 2 > max || mainTransform.position.y - adjustedScale.y / 2 < min)
        {
            localEpsilon = -.5f;
        }

        TransparentizeZWrite(min, max);
        materialManager.ModifyMaterial(MinYUntransparentized, (id, mat) => mat.SetFloat(id, min - localEpsilon));
        materialManager.ModifyMaterial(MaxYUntransparentized, (id, mat) => mat.SetFloat(id, max + localEpsilon));
    }

    protected virtual void TransparentizeZWrite(float min, float max)
    {
        materialManager.ZWriteOff();
    }

    public void DeTransparentize()
    {
        materialManager.ZWriteOn();
        materialManager.ModifyMaterial(MinYUntransparentized, (id, mat) => mat.SetFloat(id, int.MinValue));
        materialManager.ModifyMaterial(MaxYUntransparentized, (id, mat) => mat.SetFloat(id, int.MaxValue));
    }
}