


using UnityEngine;

namespace Chalk 
{
    public sealed class OnAnimateSoundPlay : MonoBehaviour
    {
        [SerializeField] private GameObject SoundPlayerPrefab;
        [SerializeField] private AudioClip PlayedSound;

        public void PlaySound()
        {
            var soundPlayer = GameObject.Instantiate(SoundPlayerPrefab).GetComponent<OneShotSoundPlayer>();
            soundPlayer.transform.position = transform.position;
            soundPlayer.transform.parent = transform;
            soundPlayer.PlaySound(PlayedSound);
        }
    }
}