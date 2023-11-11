using UnityEngine;

public class DolboidMassListener : DolboidListener
{
#pragma warning disable CS0108, CS0114
    [SerializeField] private Rigidbody rigidbody;
#pragma warning restore CS0108, CS0114
    
    protected override void HandleDolboidChanged(DolboidInfo info)
    {
        rigidbody.mass = info.Mass;
    }
}