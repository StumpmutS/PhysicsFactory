using UnityEngine;

namespace Utility.Scripts.UnityLineRenderer
{
    public class LinePointAnimator : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private PointGenerator pointGenerator;

        private int _previousCount = -1;
        
        public void Animate(float value)
        {
            value = Mathf.Clamp(value, 0f, 1f);
            var pointCount = Mathf.RoundToInt(pointGenerator.Points.Length * value);
            if (pointCount == _previousCount) return;
            _previousCount = pointCount;

            var newPoints = new Vector3[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                newPoints[i] = pointGenerator.Points[i];
            }

            lineRenderer.positionCount = pointCount;
            lineRenderer.SetPositions(newPoints);
        }
    }
}