

using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace AIAD
{
    public sealed class MusicRemainder : MonoBehaviour
    {
        [SerializeField] private AudioSource MusicSource;

        [SerializeField] private MonoBehaviour FadingSpeedProvider;

        private IConstProvider<float> ParsedFadingSpeedProvider;

        private void Awake()
        {
            ParsedFadingSpeedProvider = FadingSpeedProvider as IConstProvider<float>;
            if (ParsedFadingSpeedProvider == null)
                throw new System.NullReferenceException("Missing FadingSpeedProvider.");
        }

        public void Initialize(AudioClip clip)
        {
            MusicSource.clip = clip;
            MusicSource.Play();
        }

        private void FixedUpdate()
        {
            if (MusicSource.volume > 0)
            {
                MusicSource.volume -= ParsedFadingSpeedProvider.GetValue();
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
