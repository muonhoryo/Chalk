
using System;

namespace Chalk
{
    public interface IInteractiveObject
    {
        public event Action InteractionEvent;
        public event Action InteractionDoneEvent;
        public event Action TurnOffInteractionEvent;
        public event Action TurnOnInteractionEvent;
        public bool CanInteract_ { get; }
        public void Interact();
        public void TurnOffInteraction();
        public void TurnOnInteraction();
    }
}
