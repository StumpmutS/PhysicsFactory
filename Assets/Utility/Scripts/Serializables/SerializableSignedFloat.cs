using System;
using Utility.Scripts;

[Serializable]
public class SerializableSignedFloat
{
    public float Value;
    public bool Positive;

    public SerializableSignedFloat(float value, bool positive)
    {
        Value = value;
        Positive = positive;
    }
    
    public SerializableSignedFloat(SignedFloat signedFloat) : this(signedFloat.Value, signedFloat.Positive) { }

    public SignedFloat ToSignedFloat()
    {
        return new SignedFloat(Value, Positive);
    }
}