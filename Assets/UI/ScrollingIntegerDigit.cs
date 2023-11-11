using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScrollingIntegerDigit : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float revolutionsPerSecond = 1;
    [SerializeField] private float minRps = .1f;
    
    private float YIncrementValue => text.fontSize * (1.15f + text.lineSpacing / 100);
    private float YEdge => YIncrementValue * 5;

    private float _currentRevolution, _targetRevolution, _revolutionDifference;
    private float _speed;

    //assumes 11 elements; { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 } with 5 at y = 0
    public void SetTarget(int value)
    {
        value = Mathf.Clamp(value, 0, int.MaxValue);
        float revolutions = 0;
        float placeCounter = .1f;

        while (value != 0)
        {
            revolutions += value % 10 * placeCounter;
            value /= 10;
            placeCounter *= 10;
        }
        
        _targetRevolution = revolutions;
        _revolutionDifference = Mathf.Abs(_targetRevolution - _currentRevolution);
    }

    private void Update()
    {
        _currentRevolution = Mathf.MoveTowards(_currentRevolution, _targetRevolution,
            Mathf.Max(minRps, _revolutionDifference * revolutionsPerSecond) * Time.deltaTime);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,
            math.remap(0, 1, -YEdge, YEdge, _currentRevolution % 1));
    }
}