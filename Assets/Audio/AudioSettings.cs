using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Audio
{
    public class AudioSettings : MonoBehaviour
    {


        [SerializeField] private float masterVolume = 1f;
        [SerializeField] private float musicVolume = 0.5f;
        [SerializeField] private float sfxVolume = 0.5f;
        [SerializeField] private float ambientVolume = 0.5f;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider ambientVolumeSlider;

        private Bus master;
        private Bus music;
        private Bus sfx;
        private Bus ambient;

        private void Awake()
        {
            master = RuntimeManager.GetBus("bus:/Master");
            masterVolumeSlider.value = masterVolume;
            master.getVolume(out masterVolume);

            sfx = RuntimeManager.GetBus("bus:/Master/SFX");
            sfx.getVolume(out sfxVolume);
            sfxVolumeSlider.value = sfxVolume;

            music = RuntimeManager.GetBus("bus:/Master/Music");
            music.getVolume(out musicVolume);
            musicVolumeSlider.value = musicVolume;

            ambient = RuntimeManager.GetBus("bus:/Master/Ambience");
            ambient.getVolume(out ambientVolume);
            ambientVolumeSlider.value = ambientVolume;
        }

        private void OnEnable()
        {
            masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
            musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
            sfxVolumeSlider.onValueChanged.AddListener(SFXVolumeChanged);
            ambientVolumeSlider.onValueChanged.AddListener(AmbientVolumeChanged);
        }

        private void OnDisable()
        {
            masterVolumeSlider.onValueChanged.RemoveListener(MasterVolumeChanged);
            musicVolumeSlider.onValueChanged.RemoveListener(MusicVolumeChanged);
            sfxVolumeSlider.onValueChanged.RemoveListener(SFXVolumeChanged);
            ambientVolumeSlider.onValueChanged.RemoveListener(AmbientVolumeChanged);
        }

        private void Update()
        {
            master.setVolume(masterVolume);
            music.setVolume(musicVolume);
            sfx.setVolume(sfxVolume);
            ambient.setVolume(ambientVolume);
        }

        public void MasterVolumeChanged(float newMasterVolume)
        {
            masterVolume = newMasterVolume;
        }

        public void MusicVolumeChanged(float newMusicVolume)
        {
            musicVolume = newMusicVolume;
        }

        public void SFXVolumeChanged(float newSFXVolume)
        {
            sfxVolume = newSFXVolume;
        }

        public void AmbientVolumeChanged(float newAmbientVolume)
        {
            ambientVolume = newAmbientVolume;
        }
    }
}