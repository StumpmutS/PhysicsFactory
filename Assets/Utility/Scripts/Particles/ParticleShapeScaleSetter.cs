using System.Collections.Generic;
using UnityEngine;

namespace Utility.Scripts.Particles
{
    public class ParticleShapeScaleSetter : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> particleSystems;
        [SerializeField] private Transform referenceTransform;

        private void Start()
        {
            foreach (var particleSystem in particleSystems)
            {
                AffectParticleSystem(particleSystem, referenceTransform.localScale);
            }
        }

        protected virtual void AffectParticleSystem(ParticleSystem system, Vector3 scale)
        {
            system.SetShapeScale(scale);
        }
    }
}