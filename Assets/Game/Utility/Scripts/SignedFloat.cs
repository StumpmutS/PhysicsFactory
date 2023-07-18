using System;
using UnityEngine;

namespace Utility.Scripts
{
    public struct SignedFloat
    {
        private float _value;
        public float Value
        {
            get => _value;
            set => _value = Mathf.Abs(value);
        }

        public bool Positive;

        public SignedFloat(float value, bool positive)
        {
            _value = Mathf.Abs(value);
            Positive = positive;
        }

        public float AsFloat()
        {
            return Value * (Positive ? 1 : -1);
        }
        
        public bool Equals(SignedFloat other)
        {
            return Value == other.Value && Positive == other.Positive;
        }

        public override bool Equals(object obj)
        {
            return obj is SignedFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Positive);
        }
    }
}