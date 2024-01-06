using UnityEngine;

public abstract class ModificationComponent : MonoBehaviour
{
    public abstract void Activate(Transform mainTransform);

    public abstract void Deactivate();
}