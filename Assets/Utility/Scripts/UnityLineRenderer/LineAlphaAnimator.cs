using UnityEngine;

namespace Utility.Scripts.UnityLineRenderer
{
    public class LineAlphaAnimator : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        
        public void Animate(float value)
        {
            value = Mathf.Clamp(value, 0f, 1f);

            var gradient = lineRenderer.colorGradient;
            gradient.alphaKeys = new[] { new GradientAlphaKey(value, 0) };
            lineRenderer.colorGradient = gradient;
        }
    }
}