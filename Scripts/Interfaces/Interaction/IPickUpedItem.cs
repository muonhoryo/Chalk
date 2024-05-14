


using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Chalk
{
    public interface IPickUpedItem : IInteractiveObject
    {
        public event Action PickUpEvent;
        public event Action DropEvent;
        public event Action TurnPickingOnEvent;
        public event Action TurnPickingOffEvent;

        public void ItemInteract();
        protected IPickUpModule PickUpModule_ { get; }
        public bool CanPicked_ { get; }

        public new void Interact()
        {
            PickUpModule_.PickUpItem(this);
        }
        public void PickUp(Transform itemHandler);
        public void Drop();
        public void TurnOnPicking();
        public void TurnOffPicking();
        public void MoveToPosition(Vector3 pos);
    }
}