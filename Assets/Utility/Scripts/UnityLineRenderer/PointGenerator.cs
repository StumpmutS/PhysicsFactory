using System.Collections.Generic;
using UnityEngine;

namespace Utility.Scripts.UnityLineRenderer
{
    public abstract class PointGenerator : MonoBehaviour
    {
        [SerializeField] protected LineRenderer lineRenderer;
        
        public abstract Vector3[] Points { get; }
        
        public abstract void GeneratePoints(float lineLength);
    }
}