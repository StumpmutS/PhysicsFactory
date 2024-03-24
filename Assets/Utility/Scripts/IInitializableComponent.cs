using UnityEngine;

namespace Utility.Scripts
{
    public interface IInitializableComponent<T>
    {
        public void Init(T info);
    }
}