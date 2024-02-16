using System;

namespace Utility.Scripts
{
    public interface IFloatGroupKey
    {
        public FloatGroupEntryData EntryData { get; set; }
    }

    public struct FloatGroupEntryData
    {
        public SignedFloat FloatGroupValue;
        public float Available;

        public FloatGroupEntryData(SignedFloat floatGroupValue, float available)
        {
            FloatGroupValue = floatGroupValue;
            Available = available;
        }

        public bool Equals(FloatGroupEntryData other)
        {
            return FloatGroupValue.Equals(other.FloatGroupValue) && Available.Equals(other.Available);
        }

        public override bool Equals(object obj)
        {
            return obj is FloatGroupEntryData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloatGroupValue, Available);
        }
    }
}