using Assets.Audio;
using Assets.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Enemies
{
    [CreateAssetMenu(fileName = nameof(AggroedEnemyTracker), menuName = "ScriptableObjects/AggroedEnemyTracker")]
    public class AggroedEnemyTracker : ScriptableObject
    {
        [SerializeField] private NumAggroedEnemyChangeEvent numAggroedEnemyChangeEvent;
        AudioController audioController;
        private int numEnemies;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            numAggroedEnemyChangeEvent.AddListener(OnEnemyChangeEvent);
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            numAggroedEnemyChangeEvent.RemoveListener(OnEnemyChangeEvent);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            numEnemies = 0;
        }
        private void OnEnemyChangeEvent(object sender, EventParameters arg2)
        {
            NumEnemyChangeEventParameters enemyChangeEventParameters = (NumEnemyChangeEventParameters)arg2;
            numEnemies = Mathf.Max(0, enemyChangeEventParameters.changeValue + numEnemies);

            if (audioController == null)
                audioController = FindObjectOfType<AudioController>();
            if (audioController != null)
            {
                audioController.SetFightParameter(numEnemies > 0);
            }
        }
    }
}