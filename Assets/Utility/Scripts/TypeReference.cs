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
        public Type TargetType => Type.GetType(targetTypeAssemblyQName);
    }
}