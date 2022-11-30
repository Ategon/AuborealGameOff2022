using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities.SaveLoad
{
    [Serializable]
    public class IslandData
    {
        [SerializeField]
        public List<SerializedSavableObject> SerializedSavableObjects = new List<SerializedSavableObject>();

        public void AddSavableObject(SavableObject savableObject)
        {
            SerializedSavableObjects.Add(new SerializedSavableObject(savableObject.ID, savableObject.Position));
        }

        public bool RemoveSavableObject(SavableObject savableObjectToRemove)
        {
            SerializedSavableObject targetObject = SerializedSavableObjects.Find(so => so.ID == savableObjectToRemove.ID);

            if (targetObject != null)
            {
                SerializedSavableObjects.Remove(targetObject);
                return true;
            }

            return false;
        }

        public void Save()
        {
            SavableObject[] savableObjects = Object.FindObjectsOfType<SavableObject>();
            foreach (SavableObject savableObject in savableObjects)
            {
                SerializedSavableObject targetObject = SerializedSavableObjects.Find(so => so.ID == savableObject.ID);
                if (targetObject != null)
                {
                    savableObject.Save(targetObject);
                }
            }
        }
    }

    [Serializable]
    public class SerializedSavableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public float PositionX { get; private set; }
        [field: SerializeField] public float PositionY { get; private set; }
        [field: SerializeField] public float PositionZ { get; private set; }

        public SerializedSavableObject(int id, Vector3 position)
        {
            ID = id;
            PositionX = position.x;
            PositionY = position.y;
            PositionZ = position.z;
        }

        public void SetID(int id) => ID = id;

        public void SetPosition(Vector3 position)
        {
            PositionX = position.x;
            PositionY = position.y;
            PositionZ = position.z;
        }

        public Vector3 GetPosition()
        {
            return new Vector3(PositionX, PositionY, PositionZ);
        }
    }
}