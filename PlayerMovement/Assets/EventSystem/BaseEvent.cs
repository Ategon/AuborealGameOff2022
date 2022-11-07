using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.EventSystem
{
    public abstract class BaseEvent<TEventParameters> : ScriptableObject where TEventParameters : class
    {
        protected readonly List<Action<object, TEventParameters>> Listeners = new();

        [Header("Debug")]
        [SerializeField] protected bool ShowRaiseEvents;
        [SerializeField] protected bool ShowAddListener;
        [SerializeField] protected bool ShowRemoveListener;

        protected string Name => GetType().Name;

        internal void Raise(object sender, TEventParameters parameters = null)
        {
            if (ShowRaiseEvents)
            {
                Debug.Log($"{sender.GetType().Name} raised {Name}");
            }

            for (int i = Listeners.Count - 1; i >= 0; i--)
            {
                Listeners[i].Invoke(sender, parameters);
            }
        }

        internal void AddListener(Action<object, TEventParameters> listener)
        {
            if (ShowAddListener)
            {
                Debug.Log($"AddListener: ${listener.GetType().FullName}");
            }

            if (!Listeners.Contains(listener))
            {
                Listeners.Add(listener);
            }
        }

        internal void RemoveListener(Action<object, TEventParameters> listener)
        {
            if (ShowRemoveListener)
            {
                Debug.Log($"RemoveListener: ${listener.GetType().FullName}");
            }

            int index = Listeners.IndexOf(listener);
            if (index != -1)
            {
                Listeners.RemoveAt(index);
            }
        }
    }
}