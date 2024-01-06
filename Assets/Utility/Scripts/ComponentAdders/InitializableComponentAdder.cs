using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts.Extensions;

namespace Utility.Scripts
{
    public abstract class InitializableComponentAdder<TData> : ComponentAdder
    {
        [FormerlySerializedAs("info")] [SerializeField] private TData data;
        
        public override Component AddTo(GameObject gameObject)
        {
            return AddComponent(gameObject.AddComponent);
        }

        public override Component AddOrGetTo(GameObject gameObject)
        {
            return AddComponent(gameObject.AddOrGetComponent);
        }

        private Component AddComponent(Func<Type, Component> addMethod)
        {
            if (!ValidateType(out var type)) return null;
            var component = (IInitializableComponent<TData>) addMethod(type);
            component.Init(data);
            return (Component) component;
        }

        protected override bool CheckTypeInheritance(Type type)
        {
            return base.CheckTypeInheritance(type) &&
                   typeof(IInitializableComponent<TData>).IsAssignableFrom(type);
        }
    }
}