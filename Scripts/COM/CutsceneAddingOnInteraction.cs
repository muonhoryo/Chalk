

using Chalk.Cutscenes;
using UnityEngine;

namespace Chalk.Cutscenes
{
    public sealed class CutsceneAddingOnInteraction : MonoBehaviour
    {
        [SerializeField][Range(0,1000)] private int CutsceneIndex;
        [SerializeField] private Sprite[] Sprites;
        [SerializeField] private Sprite CutsceneIcon;
        [SerializeField] private Sprite CutsceeSelectedIcon;
        [SerializeField] private MonoBehaviour InteractiveObject;

        private IInteractiveObject ParsedInteractiveObject;
        
        private void Awake()
        {
            ParsedInteractiveObject = InteractiveObject as IInteractiveObject;
            if (ParsedInteractiveObject == null)
                throw new System.NullReferenceException("Missing InteractiveObject.");

            ParsedInteractiveObject.InteractionDoneEvent += InteractionDone;
            Destroy(this);
        }
        private void InteractionDone()
        {
            ParsedInteractiveObject.InteractionDoneEvent -= InteractionDone;
            CutsceneManager.inst_.AddCutscene(new CutsceneManager.Cutscene(CutsceneIndex,CutsceneIcon,CutsceeSelectedIcon,Sprites));
        }
    }
}