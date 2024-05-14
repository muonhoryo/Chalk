using System;
using System.Collections;
using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public sealed class Flashlight : PickUpedItem
    {
        public override event Action InteractionDoneEvent=delegate { };
        public override event Action InteractionEvent=delegate { };

        [SerializeField] private GameObject BurstEffect;
        [SerializeField] private Transform EffectOrigin;
        [SerializeField] private MonoBehaviour InteractionDelayProvider;

        private IConstProvider<float> ParsedInteractionDelayProvider;

        protected override void AwakeAction()
        {
            if (BurstEffect == null)
                throw new NullReferenceException("Missing BurstEffect.");

            ParsedInteractionDelayProvider = InteractionDelayProvider as IConstProvider<float>;
            if (ParsedInteractionDelayProvider == null)
                throw new NullReferenceException("Missing InteractionDelayProvider.");
        }

        protected override void ItemInteractAction()
        {
            PlayerController.instance_.IsActive_ = false;
            Instantiate(BurstEffect, EffectOrigin.position, EffectOrigin.rotation, EnvironmentHandler.Instance_.Environment_);
            StartCoroutine(InteractionDelay());
            InteractionEvent();
        }

        private IEnumerator InteractionDelay()
        {
            yield return new WaitForSeconds(ParsedInteractionDelayProvider.GetValue());
            PlayerController.instance_.IsActive_ = true;
            InteractionDoneEvent();
        }
    }
}
