using System.Collections.Generic;
using UnityEngine;

namespace Utility.Scripts.UnityLineRenderer
{
    public class NoisePointGenerator : PointGenerator
    {
        [SerializeField, Range(0f, 1f)] private float zNoiseAllowance;
        [SerializeField] private int minPointsPerUnit, maxPointsPerUnit;
        [SerializeField] private float minNoiseDistance, maxNoiseDistance;
        
        private Vector3[] _points = { };
        public override Vector3[] Points => _points;

        public override void GeneratePoints(float lineLength)
        {
            var pointCount = Mathf.Max(2, Mathf.FloorToInt(lineLength * Random.Range(minPointsPerUnit, maxPointsPerUnit)));
            _points = new Vector3[pointCount];
            
            var zNoise = zNoiseAllowance * (1f / pointCount);
            for (int i = 1; i < pointCount - 1; i++)
            {
                var z = (float) i / pointCount;
                _points[i] = GenerateNoise(z, -zNoise, zNoise);
            }

            _points[0] = Vector3.zero;
            _points[pointCount - 1] = Vector3.forward;
        }
        
        private Vector3 GenerateNoise(float z, float minZNoise, float maxZNoise)
        {
            var x = Random.Range(minNoiseDistance, maxNoiseDistance);
            var y = Random.Range(minNoiseDistance, maxNoiseDistance);
            z += Random.Range(minZNoise, maxZNoise);

            return new Vector3(x, y, z);
        }
    }
}