using System;
using UnityEngine;

namespace Utility.Scripts.Particles
{
    public static class ParticleHelpers
    {
        public static void SetShapeScale(this ParticleSystem particleSystem, Vector3 scale)
        {
            var shape = particleSystem.shape; 
            shape.scale = scale;
        }
        
        public static void SetEmissionRateOverTimeMultiplier(this ParticleSystem particleSystem, float multiplier)
        {
            var emission = particleSystem.emission; 
            emission.rateOverTimeMultiplier = multiplier;
        }
        
        public static void MultiplyAllBurstCount(this ParticleSystem particleSystem, int count)
        {
            var emission = particleSystem.emission;
            for (int i = 0; i < emission.burstCount; i++)
            {
                var burst = emission.GetBurst(i);
                // Assumes burst count is single constant
                burst.count = burst.maxCount * count;
                emission.SetBurst(i, burst);
            }
        }
    }
}