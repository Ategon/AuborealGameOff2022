using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.SaveLoad
{
    public class SaveLoadController : MonoBehaviourSingletonPersistent<SaveLoadController>
    {
        private const string _fileName = "SavedIslandData.sav";
        private const string _saveDirectory = "WaterWaterEverywhere";

        [SerializeField] private IslandData _islandData = new IslandData();

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene sceneObject, LoadSceneMode sceneMode)
        {
            Debug.Log($"Loaded Scene {sceneObject.name}");

            if (sceneObject.name.ToLower().Contains("level"))
            {
                Debug.Log($"Fetching Scene {sceneObject.name}");
                FetchOnSceneLoad();
            }
        }

        private void OnApplicationQuit()
        {
            SaveIslandData(_islandData);
        }

        private void FetchOnSceneLoad()
        {
            SavableObject[] savableObjects = FindObjectsOfType<SavableObject>();

            if (LoadIslandData(out IslandData islandData))
            {
                foreach (SavableObject savableObject in savableObjects)
                {
                    SerializedSavableObject targetObject = islandData.SerializedSavableObjects.Find(so => so.ID == savableObject.ID);
                    if (targetObject != null)
                    {
                        savableObject.Load(targetObject);
                    }
                }
            }
            else
            {
                _islandData = new IslandData();
                foreach (SavableObject savableObject in savableObjects)
                {
                    _islandData.AddSavableObject(savableObject);
                }
            }
        }

        private bool LoadIslandData(out IslandData islandData)
        {
            if (DoesFileExists())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    FileStream fileStream = File.Open(GetFullPath(), FileMode.Open);
                    _islandData = binaryFormatter.Deserialize(fileStream) as IslandData;
                    fileStream.Close();
                    islandData = _islandData;
                    Debug.Log($"Loaded island data {islandData.SerializedSavableObjects.Count}");
                    return true;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            islandData = null;
            return false;
        }

        private void SaveIslandData(IslandData islandData)
        {
            islandData.Save();

            if (!DoesDirectoryExists()) Directory.CreateDirectory(GetDirectoryPath());

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(GetFullPath());
            binaryFormatter.Serialize(fileStream, islandData);
            fileStream.Close();
        }

        private bool DoesFileExists()
        {
            return File.Exists(GetFullPath());
        }

        private bool DoesDirectoryExists()
        {
            return Directory.Exists(GetDirectoryPath());
        }

        private string GetFullPath()
        {
            return $"{Application.persistentDataPath}/{_saveDirectory}/{_fileName}";
        }

        private string GetDirectoryPath()
        {
            return $"{Application.persistentDataPath}/{_saveDirectory}";
        }
    }
}