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
        [SerializeField] private PlayerMeleeSwingEvent playerMeleeSwingEvent;
        [SerializeField] private PlayerMeleeHitEvent playerMeleeHitEvent;
        [SerializeField] private PlayerFootstepEvent playerFootstepEvent;
        [SerializeField] private PlayerDashEvent playerDashEvent;
        [SerializeField] private PlayerShootEvent playerShootEvent;


        private EventInstance _persistentER;
        private EventInstance _seaAmbienceER;
        private EventInstance _islandAmbienceER;
        private int lastestSceneIndex = 0;



        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (!(IsPlaying(_seaAmbienceER)))
            {
                _seaAmbienceER = RuntimeManager.CreateInstance(audioBank.ERSeaAmbience);
                _seaAmbienceER.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(FindObjectOfType<FMODUnity.StudioListener>().transform));
                _seaAmbienceER.start();
                _islandAmbienceER = RuntimeManager.CreateInstance(audioBank.ERIslandAmbience);
                _islandAmbienceER.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(FindObjectOfType<FMODUnity.StudioListener>().transform));
                _islandAmbienceER.start();
            }
            if (// IsPlaying(_persistentER) &&
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

            playerMeleeSwingEvent.AddListener(OnPlayerMeleeSwingEvent);
            playerMeleeHitEvent.AddListener(OnPlayerMeleeHitEvent);
            playerDashEvent.AddListener(OnPlayerDashEvent);
            playerFootstepEvent.AddListener(OnPlayerFootstepEvent);
            playerShootEvent.AddListener(OnPlayerShootEvent);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            playerMeleeSwingEvent.RemoveListener(OnPlayerMeleeSwingEvent);
            playerMeleeHitEvent.RemoveListener(OnPlayerMeleeHitEvent);
            playerDashEvent.RemoveListener(OnPlayerDashEvent);
            playerFootstepEvent.RemoveListener(OnPlayerFootstepEvent);
            playerShootEvent.RemoveListener(OnPlayerShootEvent);
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

        private void PlayOneShotSoundLabelled(EventReference eventReference, string labelName, string labelValue, Vector3 location = default)
        {
            EventInstance instance = RuntimeManager.CreateInstance(eventReference);
            instance.setParameterByNameWithLabel(labelName, labelValue);
            instance.start();
        }

        public void SetSeaDistance(float seaDistance)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("sea distance", 1 - seaDistance);
        }
        
        public void SetFightParameter(bool isFighting)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("fight", isFighting ? 1 : 0);
        }

        #region Events Callbacks

        private void OnPlayerMeleeSwingEvent(object sender, EventParameters parameters)
        {
            PlayOneShotSound(audioBank.ERPlayerMeleeSwing);
        }
        private void OnPlayerMeleeHitEvent(object sender, EventParameters parameters)
        {
            PlayOneShotSound(audioBank.ERPlayerMeleeHit);
        }
        private void OnPlayerFootstepEvent(object sender, EventParameters parameters)
        {
            FootstepEventParameters footstepEventParameters = parameters as FootstepEventParameters;
            PlayOneShotSoundLabelled(audioBank.ERPlayerFootsteps, footstepEventParameters.labelName, footstepEventParameters.labelValue);
        }
        private void OnPlayerShootEvent(object sender, EventParameters parameters)
        {
            PlayOneShotSound(audioBank.ERPlayerShoot);
        }
        private void OnPlayerDashEvent(object sender, EventParameters parameters)
        {
            PlayOneShotSound(audioBank.ERPlayerDash);
        }

        #endregion
    }
}