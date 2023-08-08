using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooverMatDriver : MonoBehaviour
{
#pragma warning disable CS0108, CS0114
    [SerializeField] private MeshRenderer renderer;
#pragma warning restore CS0108, CS0114
    [SerializeField] private Transform scaleReference;
    [SerializeField] private GrooverBuilding groover;

    private Material _material;
    private static readonly int FixedTime = Shader.PropertyToID("_FixedTime");
    private static readonly int Speed = Shader.PropertyToID("_Speed");
    private static readonly int Scale = Shader.PropertyToID("_Scale");

    private void Awake()
    {
        groover.OnSpeedChanged.AddListener(SetMaterial);
    }

    private void Start()
    {
        var mats = renderer.materials;
        _material = mats[0];
        renderer.materials[0] = _material;
        SetMaterial();
    }

    private void SetMaterial()
    {
        _material.SetFloat(Scale, scaleReference.localScale.z);
        _material.SetFloat(Speed, groover.CurrentSpeed);
    }

    private void FixedUpdate()
    {
        _material.SetFloat(FixedTime, Time.fixedTime);
    }

    private void OnDestroy()
    {
        if (groover != null) groover.OnSpeedChanged.RemoveListener(SetMaterial);
    }
}