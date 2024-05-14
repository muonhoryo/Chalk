using System;
using Chalk.Player;
using UnityEngine;

namespace Chalk
{
    public sealed class Cup : PickUpedItem
    {
        public override event Action InteractionDoneEvent = delegate { };
        public override event Action InteractionEvent = delegate { };

        [SerializeField] private Animator animator;
        [SerializeField] private string TriggerAnimName;

        protected override void AwakeAction()
        {
            if (animator == null)
                throw new NullReferenceException("Missing Animator.");
            if (string.IsNullOrEmpty(TriggerAnimName))
                throw new NullReferenceException("Missing TriggerAnimName.");
        }

        protected override void ItemInteractAction()
        {
            PlayerController.instance_.IsActive_ = false;
            animator.SetTrigger(TriggerAnimName);
            InteractionEvent();
        }

        public void AnimationDone()
        {
            PlayerController.instance_.IsActive_ = true;
            InteractionDoneEvent.Invoke();
        }
    }
}
