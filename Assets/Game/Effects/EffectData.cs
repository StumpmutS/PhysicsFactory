using UnityEngine;

public struct EffectData
{
    public GameObject GameObject;
    public Vector3 EffectOrigin;

    public EffectData(GameObject gameObject, Vector3 effectOrigin)
    {
        GameObject = gameObject;
        EffectOrigin = effectOrigin;
    }
}