using UnityEngine;
using Utility.Scripts;

public class SceneCreatableReference : Singleton<SceneCreatableReference>
{
    [SerializeField] private CreatableController controller;
    public CreatableController CreatableController => controller;
}