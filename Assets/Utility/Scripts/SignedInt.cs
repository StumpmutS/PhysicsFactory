using System;

namespace Utility.Scripts
{
    //So that zero can be positive or negative
    public struct SignedInt
    {
        public uint Value;
        public bool Positive;

        public SignedInt(uint value, bool positive)
        {
            Value = value;
            Positive = positive;
        }

        public int AsInt()
        {
            return (int) Value * (Positive ? 1 : -1);
        }
        
        public bool Equals(SignedInt other)
        {
            return Value == other.Value && Positive == other.Positive;
        }

        public override bool Equals(object obj)
        {
            return obj is SignedInt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Positive);
        }
    }
}