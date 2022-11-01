using System;
using UnityEngine;
using Auboreal.Core.EventSystem;

namespace Auboreal.Unity.EventSystem
{
    public class SceneTreeEventNode : MonoBehaviour
    {
        private SceneTreeEventNode parent;
        private EventDirectory dir;
        public EventDirectory Directory { get => dir; }
        private ForwardsPreOrderEventCarrier forwardsCarrier;
        private BackwardsPreOrderEventCarrier backwardsCarrier;

        void Awake()
        {
            dir = new EventDirectory();
            forwardsCarrier = new ForwardsPreOrderEventCarrier();
            backwardsCarrier = new BackwardsPreOrderEventCarrier();
        }

        void Start()
        {
            Connect();
            IEventHandler[] eventHandler = GetComponents<IEventHandler>();
            Directory.Handlers.AddRange(eventHandler);
        }

        void OnTransformParentChanged()
        {
            SceneTreeEventNode nextNode = GetNextDirectory();

            if (nextNode != parent)
            {
                Disconnect();
                Connect();
            }
        }

        public void Bubble(IEvent e)
        {
            backwardsCarrier.DeliverEvent(dir, e);
        }

        public void Trickle(IEvent e)
        {
            forwardsCarrier.DeliverEvent(dir, e);
        }

        public void Submit(IEvent e)
        {
            forwardsCarrier.SubmitEvent(dir, e);
        }

        public void Connect()
        {
            SceneTreeEventNode next = GetNextDirectory();
            if (next != null)
            {
                parent = next;
                next.dir.ConnectBidirectional(dir);
            }
        }

        public void Disconnect()
        {
            if (parent != null)
            {
                parent.dir.DisconnectBidirectional(dir);
                parent = null;
            }
        }

        public SceneTreeEventNode GetNextDirectory()
        {
            SceneTreeEventNode nextDir = null;
            GameObject currentObj;
            bool isRoot = false;
            bool hasNextDir = false;

            currentObj = this.gameObject;
            while (!hasNextDir && !isRoot)
            {
                Transform parent = currentObj.transform.parent;
                if (parent != null)
                {
                    currentObj = parent.gameObject;
                    hasNextDir = currentObj.TryGetComponent(out nextDir);
                }
                else
                {
                    isRoot = true;
                }
            }

            return nextDir;
        }
    }
}