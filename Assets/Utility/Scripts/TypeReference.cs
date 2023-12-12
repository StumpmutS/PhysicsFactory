using System;
using UnityEngine;

namespace Utility.Scripts
{
    [Serializable]
    public class TypeReference
    {
        [SerializeField, HideInInspector] private string searchKeyWord;
        [SerializeField, HideInInspector] private string targetTypeAssemblyQName;
        public string TargetTypeAssemblyQualifiedName => targetTypeAssemblyQName;
        public Type TargetType
        {
            get
            {
                var type = Type.GetType(targetTypeAssemblyQName);
                if (type == null)
                {
                    Debug.LogError($"Could not find type with assembly qualified name: {targetTypeAssemblyQName}, " +
                                   "make sure you did not rename the type you are looking for");
                }

                return type;
            }
        }

        public TypeReference(Type type)
        {
            SetTargetType(type);
        }
        
        public void SetTargetType(Type type)
        {
            searchKeyWord = type.Name;
            targetTypeAssemblyQName = type.AssemblyQualifiedName;
        }
    }
}