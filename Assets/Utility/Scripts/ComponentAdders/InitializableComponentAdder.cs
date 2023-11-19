using System;
using UnityEngine;

namespace Utility.Scripts
{
    public abstract class InitializableComponentAdder<TInfo> : ComponentAdder
    {
        [SerializeField] private TInfo info;
        
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
            var component = (IInitializableComponent<TInfo>) addMethod(type);
            component.Init(info);
            return (Component) component;
        }

        protected override bool CheckTypeInheritance(Type type)
        {
            return base.CheckTypeInheritance(type) &&
                   typeof(IInitializableComponent<TInfo>).IsAssignableFrom(type);
        }
    }
}