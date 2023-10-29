using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooverMatDriver : MonoBehaviour
{
    [SerializeField] private MaterialManager materialManager;
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
        SetMaterial();
    }

    private void SetMaterial()
    {
        materialManager.ModifyMaterial(Scale, (id, material) => material.SetFloat(id, scaleReference.localScale.z));
        materialManager.ModifyMaterial(Speed, (id, material) => material.SetFloat(id, groover.CurrentSpeed));
    }

    private void FixedUpdate()
    {
        materialManager.ModifyMaterial(FixedTime, (id, material) => material.SetFloat(id, Time.fixedTime));
    }

    private void OnDestroy()
    {
        if (groover != null) groover.OnSpeedChanged.RemoveListener(SetMaterial);
    }
}