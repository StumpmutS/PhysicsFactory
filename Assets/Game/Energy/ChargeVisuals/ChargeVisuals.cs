﻿using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Utility.Scripts;

public abstract class ChargeVisuals : MonoBehaviour
{
    [SerializeField] private GameObject visualsContainer;
    [SerializeField] private TMP_Text text;
    
    protected abstract string TextValue { get; }
    
    public void Activate()
    {
        visualsContainer.SetActive(true);
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = TextValue;
    }
    
    public void Deactivate()
    {
        visualsContainer.SetActive(false);
    }
}