

using MuonhoryoLibrary.Unity.COM;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chalk
{
    public abstract class PickUpedItem : MonoBehaviour, IPickUpedItem
    {
        public event Action PickUpEvent = delegate { };
        public event Action DropEvent = delegate { };
        public abstract event Action InteractionEvent;
        public abstract event Action InteractionDoneEvent;
        public event Action TurnOnInteractionEvent = delegate { };
        public event Action TurnOffInteractionEvent = delegate { };
        public event Action TurnPickingOnEvent = delegate { };
        public event Action TurnPickingOffEvent = delegate { };

        [SerializeField] protected Rigidbody rgbody;
        [SerializeField] protected Collider AttachedCollider;

        [SerializeField] private MonoBehaviour HandledPositionProvider;
        [SerializeField] private MonoBehaviour HandledRotationProvider;
        [SerializeField] private MonoBehaviour PickUpModule;

        protected IConstProvider<Vector3> ParsedHandledPositionProvider;
        protected IConstProvider<Vector3> ParsedHandledRotationProvider;
        protected IPickUpModule ParsedPickUpModule;

        public bool CanInteract_ { get; private set; } = true;
        public bool CanPicked_ { get; private set; } = true;

        private void Awake()
        {
            if (rgbody == null)
                throw new NullReferenceException("Missing Rigidbody.");

            if (AttachedCollider == null)
                throw new NullReferenceException("Missing AttachedCollider.");

            ParsedHandledPositionProvider = HandledPositionProvider as IConstProvider<Vector3>;
            if (ParsedHandledPositionProvider == null)
                throw new NullReferenceException("Missing HandledPositionProvider.");

            ParsedHandledRotationProvider = HandledRotationProvider as IConstProvider<Vector3>;
            if (ParsedHandledRotationProvider == null)
                throw new NullReferenceException("Missing HandledRotationProvider.");

            ParsedPickUpModule = PickUpModule as IPickUpModule;
            if (ParsedPickUpModule == null)
                throw new NullReferenceException("Missing PickUpModule.");

            AwakeAction();
        }

        public void Interact()
        {
            if (CanPicked_)
            {
                ParsedPickUpModule.PickUpItem(this);
            }
        }
        public virtual void PickUp(Transform itemHandler)
        {
            TurnPhysics(false);
            transform.SetParent(itemHandler, true);
            transform.localPosition = ParsedHandledPositionProvider.GetValue();
            transform.localEulerAngles = ParsedHandledRotationProvider.GetValue();
        }
        public virtual void Drop()
        {
            TurnPhysics(true);
            transform.SetParent(EnvironmentHandler.Instance_.Environment_, true);
            DropEvent();
        }
        protected void TurnPhysics(bool activity)
        {
            rgbody.isKinematic = !activity;
            AttachedCollider.enabled = activity;
        }
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
        public void TurnOnPicking()
        {
            if (!CanPicked_)
            {
                CanPicked_ = true;
                TurnPickingOnEvent();
            }
        }
        public void TurnOffPicking()
        {
            if (CanPicked_)
            {
                CanPicked_ = false;
                TurnPickingOffEvent();
            }
        }
        public void MoveToPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void ItemInteract()
        {
            if (CanInteract_)
            {
                ItemInteractAction();
            }
        }
        protected abstract void ItemInteractAction();
        protected virtual void AwakeAction() { }

        IPickUpModule IPickUpedItem.PickUpModule_ => ParsedPickUpModule;
    }
}