using Assets.Audio.Events;
using Assets.EventSystem;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Assets.Audio
{
    public class AudioController : MonoBehaviourSingletonPersistent<AudioController>
    {
        [SerializeField] private AudioBank audioBank;

        [Header("Events")]
        [SerializeField] private PlayerMeleeAttackEvent playerMeleeAttackEvent;


        private EventInstance _persistentER;
        private int lastestSceneIndex = 0;


        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (IsPlaying(_persistentER) &&
                lastestSceneIndex <= 1 &&
                scene.buildIndex <= 1)
            {
                lastestSceneIndex = scene.buildIndex;
                return;
            }

            StopPersistentMusic();

            var levelId = SceneManager.GetActiveScene().buildIndex;
            PlayPersistentMusic(audioBank.ERLevels[levelId], levelId);

            lastestSceneIndex = scene.buildIndex;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            playerMeleeAttackEvent.AddListener(OnPlayerMeleeAttackEvent);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            playerMeleeAttackEvent.RemoveListener(OnPlayerMeleeAttackEvent);
        }

        private bool IsPlaying(FMOD.Studio.EventInstance instance)
        {
            instance.getPlaybackState(out var state);
            return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
        }

        private void PlayPersistentMusic(EventReference eventReference, int levelId)
        {
            _persistentER = RuntimeManager.CreateInstance(eventReference);
            _persistentER.start();

            if (levelId > 2)
                _persistentER.setParameterByName("prop", Random.Range(0.55f, 1f));
        }

        private void OnDestroy()
        {
            StopPersistentMusic();
        }

        private void StopPersistentMusic()
        {
            _persistentER.stop(STOP_MODE.ALLOWFADEOUT);
            _persistentER.release();
        }

        private void PlayOneShotSound(EventReference eventReference, Vector3 location = default)
        {
            RuntimeManager.PlayOneShot(eventReference, location);
        }

        #region Events Callbacks

        private void OnPlayerMeleeAttackEvent(object sender, EventParameters parameters)
        {
            PlayOneShotSound(audioBank.ERPlayerMeleeAttack);
        }

        #endregion
    }
}