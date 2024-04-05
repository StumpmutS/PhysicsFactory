using UnityEngine;
using Utility.Scripts.Extensions;

namespace Utility.Scripts.Particles
{
    public class ParticleBurstScaleSetter : ParticleShapeScaleSetter
    {
        protected override void AffectParticleSystem(ParticleSystem system, Vector3 scale)
        {
            base.AffectParticleSystem(system, scale);
            system.MultiplyAllBurstCount(Mathf.RoundToInt(scale.Volume()));
        }
    }
}