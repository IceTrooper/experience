using System;
using UnityEngine;

namespace Atoms
{
    [CreateAssetMenu(fileName = "BaseEvent", menuName = "IceTrooper/Atoms/Events/Base")]
    public class AtomEvent : ScriptableObject
    {
        private event Action OnEventBase;

        public virtual void Raise()
        {
            OnEventBase?.Invoke();
        }

        public void Register(Action action)
        {
            OnEventBase += action;
        }

        public void Unregister(Action action)
        {
            OnEventBase -= action;
        }
    }

    public class AtomEvent<T> : AtomEvent
    {
        private event Action<T> OnEvent;

        public void Raise(T item)
        {
            base.Raise();
            OnEvent?.Invoke(item);
        }

        public void Register(Action<T> action)
        {
            OnEvent += action;
        }

        public void Unregister(Action<T> action)
        {
            OnEvent -= action;
        }
    }
}
