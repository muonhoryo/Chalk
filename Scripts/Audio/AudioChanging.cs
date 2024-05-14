

using UnityEngine;

namespace Chalk
{
    public sealed class AudioChanging : MonoBehaviour
    {
        public enum AudioType
        {
            Sounds,
            Music
        }

        [SerializeField] private AudioType Type;

        public void ChangeAudioVolume(float volume)
        {
            if(Type== AudioType.Sounds)
            {
                AudioSettings.inst_.SoundLevel_ = volume;
            }
            else
            {
                AudioSettings.inst_.MusicLevel_ = volume;
            }
        }
    }
}