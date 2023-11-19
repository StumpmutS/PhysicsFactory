using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Scripts
{
    [Serializable]
    public class TypeReferenceListWrapper
    {
        [SerializeField] private List<TypeReference> typeReferences;
        public List<TypeReference> TypeReferences => typeReferences;
    }
}