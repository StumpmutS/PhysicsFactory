using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utility.Scripts
{
    public static class StumpComponentExtensions
    {
        public static T AddOrGetComponent<T>(this Component component) where T : Component
        {
            if (!component.TryGetComponent<T>(out var found))
            {
                found = component.gameObject.AddComponent<T>();
            }

            return found;
        }
        
        public static Component AddOrGetComponent(this Component component, Type componentType)
        {
            if (!component.TryGetComponent(componentType, out var found))
            {
                found = component.gameObject.AddComponent(componentType);
            }

            return found;
        }
        
        public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent<T>(out var found))
            {
                found = gameObject.gameObject.AddComponent<T>();
            }

            return found;
        }
        
        public static Component AddOrGetComponent(this GameObject gameObject, Type componentType)
        {
            if (!gameObject.TryGetComponent(componentType, out var found))
            {
                found = gameObject.AddComponent(componentType);
            }

            return found;
        }

        public static bool TryGetComponentInChildren<T>(this Component component, out T result) where T : Component
        {
            result = component.GetComponentInChildren<T>();
            return result != null;
        }

        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T result) where T : Component
        {
            result = gameObject.GetComponentInChildren<T>();
            return result != null;
        }

        public static bool RemoveOneComponent<T>(this Component component) where T : Component
        {
            var found = component.GetComponent<T>();
            if (found == null) return false;
            
            Object.Destroy(found);
            return true;
        }

        public static bool RemoveAllComponents<T>(this Component component) where T : Component
        {
            var components = component.GetComponents<T>();
            foreach (var found in components)
            {
                Object.Destroy(found);
            }

            return components.Length > 0;
        }
    }
}