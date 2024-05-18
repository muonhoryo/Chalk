


using UnityEngine;

namespace Chalk
{
    public sealed class TurnedSound : MonoBehaviour 
    {
        [SerializeField] private MonoBehaviour InteractiveObject;
        [SerializeField] private AudioSource AudioSource;

        private void Awake()
        {
            var parsedObj = InteractiveObject as IInteractiveObject;
            if (parsedObj == null)
                throw new System.NullReferenceException("Missing InteractiveObject.");

            parsedObj.InteractionEvent += OnInteraction;
        }
        private void OnInteraction()
        {
            if (AudioSource.isPlaying)
                AudioSource.Stop();
            else
                AudioSource.Play();
        }
    }

}