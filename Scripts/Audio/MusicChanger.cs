using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace AIAD
{
    public sealed class MusicChanger : MonoBehaviour
    {
        [SerializeField] private AudioSource MusicSource;
        [SerializeField] private GameObject MusicRemainderPrefab;
        [SerializeField] private MonoBehaviour FadingSpeedProvider;

        private IConstProvider<float> ParsedFadingSpeedProvider;

        public AudioClip PlayedMusic_ => MusicSource.clip;
        private void Awake()
        {
            ParsedFadingSpeedProvider = FadingSpeedProvider as IConstProvider<float>;
            if (ParsedFadingSpeedProvider == null)
                throw new System.NullReferenceException("Missing FadingSpeedProvider.");
        }

        public void SetMusic(AudioClip musicClip)
        {
            MusicRemainder remScript = Instantiate(MusicRemainderPrefab, gameObject.transform).GetComponent<MusicRemainder>();
            remScript.Initialize( MusicSource.clip);
            MusicSource.clip = musicClip;
            MusicSource.Play();
            StopAllCoroutines();
            float targetVolume = MusicSource.volume;
            MusicSource.volume = 0;
            StartCoroutine(VolumeUnfading(targetVolume));
        }

        private IEnumerator VolumeUnfading(float targetVolume)
        {
            while (true)
            {
                MusicSource.volume += ParsedFadingSpeedProvider.GetValue();
                if (MusicSource.volume >= targetVolume)
                {
                    MusicSource.volume = targetVolume;
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}