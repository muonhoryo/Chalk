using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk
{
    public class OnInteractionDoneSoundPlay : MonoBehaviour
    {
        [SerializeField] private GameObject SoundPlayerPrefab;
        [SerializeField] private AudioClip Sound;
        [SerializeField] private MonoBehaviour InteractiveObject;

        private void Awake()
        {
            var parsedObject = InteractiveObject as IInteractiveObject;
            if (parsedObject == null)
                throw new System.NullReferenceException("Missing InteractiveObject.");
            if (Sound == null)
                throw new System.NullReferenceException("Missing Sound.");
            if (SoundPlayerPrefab == null)
                throw new System.NullReferenceException("Missing SoundPlayerPrefab.");

            Subscribe(parsedObject);
        }
        protected void PlaySound()
        {
            var soundPlayer = GameObject.Instantiate(SoundPlayerPrefab).GetComponent<OneShotSoundPlayer>();
            soundPlayer.transform.position = InteractiveObject.transform.position;
            soundPlayer.transform.parent = InteractiveObject.transform;
            soundPlayer.PlaySound(Sound);
        }
        protected virtual void Subscribe(IInteractiveObject interactiveObj)
        {
            interactiveObj.InteractionDoneEvent += PlaySound;
        }
    }
}
