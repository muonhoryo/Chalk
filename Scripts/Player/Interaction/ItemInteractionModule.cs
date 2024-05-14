


using System;
using UnityEngine;

namespace Chalk
{
    public sealed class ItemInteractionModule :MonoBehaviour,IPickUpModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };
        public event Action<IPickUpedItem> PickUpItemEvent = delegate { };
        public event Action<IPickUpedItem> DropItemEvent = delegate { };
        public event Action InteractiveObjHasBeenLostEvent = delegate { };
        public event Action<IInteractiveObject> InteractionEvent = delegate { };

        [SerializeField] private Transform ItemHandler;

        public IPickUpedItem HandledItem_ { get; private set; }

        public Transform ItemHandler_=>ItemHandler;

        private void Awake()
        {
            if (ItemHandler == null)
                throw new NullReferenceException("Missing ItemHandler.");
        }

        public void PickUpItem(IPickUpedItem item)
        {
            if (HandledItem_ == null)
            {
                HandledItem_ = item;
                HandledItem_.PickUp(ItemHandler);
                HandledItem_.DropEvent += OnItemselftDropAction;
                PickUpItemEvent(item);
            }
        }
        public void DropItem()
        {
            if (HandledItem_ != null)
            {
                HandledItem_.DropEvent -= OnItemselftDropAction;
                HandledItem_.Drop();
                DropAction();
            }
        }
        private void DropAction()
        {
            var item = HandledItem_;
            Vector3 fromUsToHandler = ItemHandler.position - transform.position;
            if (Physics.Raycast(
                origin: transform.position,
                direction: fromUsToHandler.normalized,
                hitInfo: out RaycastHit info,
                maxDistance: fromUsToHandler.magnitude,
                layerMask: 1,
                queryTriggerInteraction: QueryTriggerInteraction.Ignore))
            {
                item.MoveToPosition(info.point);
            }
            HandledItem_ = null;
            DropItemEvent(item);
        }
        private void OnItemselftDropAction()
        {
            if (HandledItem_ != null)
            {
                DropAction();
            }
        }

        public void Interact()
        {
            if (HandledItem_ != null)
            {
                HandledItem_.ItemInteract();
                InteractionEvent(HandledItem_);
            }
        }
    }
}