


using System;

namespace Chalk.Player
{
    public interface IInteractionModule
    {
        public event Action<IInteractiveObject> InteractiveObjHasBeenFoundEvent;
        public event Action InteractiveObjHasBeenLostEvent;
        public event Action<IInteractiveObject> InteractionEvent;
        public void Interact();
    }
}