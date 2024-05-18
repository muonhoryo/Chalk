


namespace Chalk 
{
    public sealed class OnInteractionSoundPlay : OnInteractionDoneSoundPlay 
    {
        protected override void Subscribe(IInteractiveObject interactiveObj)
        {
            interactiveObj.InteractionEvent += PlaySound;
        }
    }
}