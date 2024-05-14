using System;
using UnityEngine;
using UnityEngine.Audio;
using MuonhoryoLibrary.Unity;

namespace Chalk
{
    public sealed class AudioSettings : MonoBehaviour
    {
        public static AudioSettings inst_ { get; private set; }

        public event Action<float> SoundsLevelChangedEvent = delegate { };
        public event Action<float> MusicLevelChangedEvent = delegate { };

        [SerializeField] private string SoundsLevelName;
        [SerializeField] private string MusicLevelName;

        [SerializeField] private AudioMixer AudioMixer;

        private float SoundLevel=1;
        private float MusicLevel = 1;

        public float SoundLevel_ 
        {
            get=>SoundLevel;
            set
            {
                SoundLevel = Mathf.Clamp01(value);
                AudioMixer.SetFloat(SoundsLevelName, SoundLevel.VolumeLevelToDB());
                SoundsLevelChangedEvent(SoundLevel);
            }
        }
        public float MusicLevel_ 
        {
            get=>MusicLevel;
            set
            {
                MusicLevel = Mathf.Clamp01(value);
                AudioMixer.SetFloat(MusicLevelName, MusicLevel.VolumeLevelToDB());
                MusicLevelChangedEvent(MusicLevel);
            }
        }

        private void Awake()
        {
            if (string.IsNullOrEmpty(SoundsLevelName))
                throw new NullReferenceException("Missing SoundsLevelName.");
            if (string.IsNullOrEmpty(MusicLevelName))
                throw new NullReferenceException("Missing MusicLevelName.");

            if (AudioMixer == null)
                throw new NullReferenceException("Missing AudioMixer.");

            inst_ = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
