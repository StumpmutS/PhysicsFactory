using UnityEngine;

public class DefaultCreatable : Creatable
{
    public override Creatable Create()
    {
        return Instantiate(this);
    }

    public override Creatable Create(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public override Creatable Create(Transform parent, bool instantiateInWorldSpace)
    {
        return Instantiate(this);
    }

    public override Creatable Create(Vector3 position, Quaternion rotation)
    {
        return Instantiate(this, position, rotation);
    }

    public override Creatable Create(Vector3 position, Quaternion rotation, Transform parent)
    {
        return Instantiate(this, position, rotation, parent);
    }

    public override void Dispose()
    {
        Destroy(gameObject);
    }
}