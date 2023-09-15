﻿using System;
using UnityEngine;

[Serializable]
public class PreviewRendererInfo
{
    [SerializeField] private bool overrideAlpha;
    public bool OverrideAlpha => overrideAlpha;
    [SerializeField] private float alpha;
    public float Alpha => alpha;
    [SerializeField] private Renderer renderer;
    public Renderer Renderer => renderer;
}