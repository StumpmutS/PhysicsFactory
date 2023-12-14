using UnityEngine;

public class TimeScaleEditor : MonoBehaviour
{
    [SerializeField] private float timeScale = 1;
    
    private void Update()
    {
        Time.timeScale = timeScale;
    }
}