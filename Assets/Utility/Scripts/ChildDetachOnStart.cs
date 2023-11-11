using UnityEngine;

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