using System;
using UnityEngine;

namespace Utility.Scripts
{
    //So that zero can be positive or negative
    public struct SignedInt
    {
        private int _value;
        public int Value
        {
            get => _value;
            set => _value = Mathf.Abs(value);
        }
        
        public bool Positive;

        public SignedInt(int value, bool positive)
        {
            _value = Mathf.Abs(value);
            Positive = positive;
        }

        public int AsInt()
        {
            return Value * (Positive ? 1 : -1);
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