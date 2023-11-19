using System;
using UnityEngine;

namespace Utility.Scripts
{
    [CreateAssetMenu(menuName = "ComponentAdders/DefaultAdder")]
    public class ComponentAdder : ScriptableObject
    {
        [SerializeField] protected TypeReference typeReference;

        public virtual Component AddTo(GameObject gameObject)
        {
            return ValidateType(out var type) ? gameObject.AddComponent(type) : null;
        }

        public virtual Component AddOrGetTo(GameObject gameObject)
        {
            return ValidateType(out var type) ? gameObject.AddOrGetComponent(type) : null;
        }

        protected bool ValidateType(out Type type)
        {
            type = typeReference.TargetType;

            if (CheckTypeInheritance(type)) return true;
            
            Debug.LogError("Type does not inherit from necessary classes");
            return false;
        }

        protected virtual bool CheckTypeInheritance(Type type)
        {
            return typeof(Component).IsAssignableFrom(type);
        }
    }
}