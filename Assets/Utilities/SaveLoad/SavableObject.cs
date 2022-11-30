using System;
using UnityEngine;

namespace Utilities.SaveLoad
{
    [Serializable]
    public abstract class SavableObject : MonoBehaviour
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public virtual void Load(SerializedSavableObject serializedSavableObject)
        {
            Position = serializedSavableObject.GetPosition();
            this.transform.position = Position;
        }

        public virtual void Save(SerializedSavableObject serializedSavableObject)
        {
            Position = this.gameObject.transform.position;
            serializedSavableObject.SetID(this.ID);
            serializedSavableObject.SetPosition(this.Position);
        }
    }
}