using UnityEngine;

public struct EffectData
{
    public GameObject GameObject;
    public Vector3 Origin;

    public EffectData(GameObject gameObject, Vector3 origin)
    {
        GameObject = gameObject;
        Origin = origin;
    }
}