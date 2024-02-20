using UnityEngine;

public class PacketConnectionVisual : MonoBehaviour
{
#pragma warning disable CS0108, CS0114
    [SerializeField] private ParticleSystem particleSystem;
#pragma warning restore CS0108, CS0114
    public ParticleSystem ParticleSystem => particleSystem;
}