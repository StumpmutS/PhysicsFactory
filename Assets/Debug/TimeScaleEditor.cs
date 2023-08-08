using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleEditor : MonoBehaviour
{
    [SerializeField] private float timeScale = 1;
    
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
