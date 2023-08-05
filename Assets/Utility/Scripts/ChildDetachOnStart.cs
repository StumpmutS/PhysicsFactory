using System;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Utility.Scripts
{
    public class ChildDetachOnStart : MonoBehaviour
    {
        private void Start()
        {
            transform.SetParent(null, true);
        }
    }
}