using FMODUnity;
using UnityEngine;

namespace Assets.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioBank), menuName = "ScriptableObjects/AudioBank")]
    public class AudioBank : ScriptableObject
    {
        [field: Header("SFX")]
        [field: SerializeField] public EventReference ERPlayerMeleeAttack { get; private set; }

        [field: Header("BGM")]
        [field: SerializeField] public EventReference[] ERLevels { get; private set; }

    }
}