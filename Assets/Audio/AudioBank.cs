using FMODUnity;
using UnityEngine;

namespace Assets.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioBank), menuName = "ScriptableObjects/AudioBank")]
    public class AudioBank : ScriptableObject
    {
        [field: Header("SFX")]
        [field: SerializeField] public EventReference ERPlayerMeleeSwing { get; private set; }
        [field: SerializeField] public EventReference ERPlayerMeleeHit { get; private set; }
        [field: SerializeField] public EventReference ERPlayerFootsteps { get; private set; }
        [field: SerializeField] public EventReference ERPlayerShoot { get; private set; }
        [field: SerializeField] public EventReference ERPlayerDash { get; private set; }

        [field: Header("BGM")]
        [field: SerializeField] public EventReference[] ERLevels { get; private set; }

    }
}