


using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
using System;
using UnityEngine;

namespace Chalk
{
    public abstract class StaticAnimatedObj : MonoBehaviour, IInteractiveObject
    {
        public event Action InteractionEvent = delegate { };
        public event Action InteractionDoneEvent = delegate { };
        public event Action TurnOnInteractionEvent = delegate { };
        public event Action TurnOffInteractionEvent = delegate { };

        [SerializeField] private Animator animator;
        [SerializeField] private string TriggerName;
        [SerializeField] private Transform AnimatedObj;
        [SerializeField] private CameraTargetFollowingRotationModule FollowingModule;

        public bool CanInteract_ { get; private set; } = true;

        private void Awake()
        {
            if (string.IsNullOrEmpty(TriggerName))
                throw new NullReferenceException("Missing TriggerName.");
            if (animator == null)
                throw new NullReferenceException("Missing Animator.");
            if (AnimatedObj == null)
                throw new NullReferenceException("Missing AnimatedObj.");
            if (FollowingModule == null)
                throw new NullReferenceException("Missing FollowingModule.");

            AwakeAction();
        }
        protected virtual void AwakeAction() { }

        public void Interact()
        {
            if (CanInteract_)
            {
                PlayerController.instance_.IsActive_ = false;
                FollowingModule.SetTarget(AnimatedObj);
                FollowingModule.IsActive_ = true;
                animator.SetTrigger(TriggerName);
                InteractionAction();
                InteractionEvent();
            }
        }
        protected virtual void InteractionAction() { }
    
        public void TurnOnInteraction()
        {
            if (!CanInteract_)
            {
                CanInteract_ = true;
                TurnOnInteractionEvent();
            }
        }
        public void TurnOffInteraction()
        {
            if (CanInteract_)
            {
                CanInteract_ = false;
                TurnOffInteractionEvent();
            }
        }

        public void AnimationDone()
        {
            PlayerController.instance_.IsActive_ = true;
            FollowingModule.IsActive_ = false;
            AnimationDoneAction();
            InteractionDoneEvent();
        }
        protected virtual void AnimationDoneAction() { }
    }
}