using UnityEngine;
using Utility.Scripts.Extensions;

namespace Utility.Scripts.Particles
{
    public class ParticleEmissionRateScaleSetter : ParticleShapeScaleSetter
    {
        protected override void AffectParticleSystem(ParticleSystem system, Vector3 scale)
        {
            base.AffectParticleSystem(system, scale);
            system.SetEmissionRateOverTimeMultiplier(system.emission.rateOverTimeMultiplier * scale.Volume());
        }
    }
}