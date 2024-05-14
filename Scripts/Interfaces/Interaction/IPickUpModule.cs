

using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
using System;
using UnityEngine;

namespace Chalk
{
    public interface IPickUpModule : IInteractionModule
    {
        public event Action<IPickUpedItem> PickUpItemEvent;
        public event Action<IPickUpedItem> DropItemEvent;

        event Action<IInteractiveObject> IInteractionModule.InteractiveObjHasBeenFoundEvent
        { add => PickUpItemEvent += value;remove => PickUpItemEvent -= value; }

        public Transform ItemHandler_ { get; }

        public void PickUpItem(IPickUpedItem item);
        public void DropItem();
    }
}