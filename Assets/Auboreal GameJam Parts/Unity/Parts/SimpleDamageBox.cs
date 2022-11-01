using System;
using System.Collections.Generic;
using UnityEngine;
using Auboreal.Core.EventSystem;
using Auboreal.Unity.EventSystem;

namespace Auboreal.Unity
{
    [RequireComponent(typeof(Collider2D))]
    public class SimpleDamageBox : MonoBehaviour
    {
        private new Collider2D collider;
        List<Collider2D> colliders;

        public int damage;
        public float time;
        private float timeElapsed;

        void Start()
        {
            timeElapsed = 0;
            TryGetComponent<Collider2D>(out collider);
            colliders = new List<Collider2D>();
        }

        private void FixedUpdate()
        {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed >= time)
            {
                colliders.Clear();
                collider.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
                DamageAll(colliders);
                timeElapsed = 0;
            }
        }

        private void DamageAll(List<Collider2D> colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                Damage(collider);
            }
        }

        private void Damage(Collider2D other)
        {
            SceneTreeEventNode eventNode;
            bool hasEventNode = other.TryGetComponent<SceneTreeEventNode>(out eventNode);
            if (hasEventNode)
            {
                SimpleDamageEvent e = new SimpleDamageEvent(damage);
                eventNode.Submit(e);
                eventNode.Trickle(e);
            }
        }
    }

    public class SimpleDamageEvent : IEvent
    {
        public SimpleDamageData damage;

        public SimpleDamageEvent(int damage)
        {
            this.damage = new SimpleDamageData
            {
                Damage = damage
            };
        }
    }
}