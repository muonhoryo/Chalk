

using System.Collections;
using AIAD;
using Chalk.Cutscenes;
using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
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
        [SerializeField] private AudioClip PlayedMusic;
        [SerializeField] private MonoBehaviour SceneShowingDelayProvider;

        private IConstProvider<float> ParsedSceneShowingDelayProvider;
        private IInteractiveObject ParsedInteractiveObject;
        
        private void Awake()
        {
            ParsedInteractiveObject = InteractiveObject as IInteractiveObject;
            if (ParsedInteractiveObject == null)
                throw new System.NullReferenceException("Missing InteractiveObject.");

            ParsedSceneShowingDelayProvider = SceneShowingDelayProvider as IConstProvider<float>;
            if (ParsedSceneShowingDelayProvider == null)
                throw new System.NullReferenceException("Missing SceneShowingDelayProvider");

            ParsedInteractiveObject.InteractionDoneEvent += InteractionDone;
        }
        private void InteractionDone()
        {
            ParsedInteractiveObject.InteractionDoneEvent -= InteractionDone;
            StartCoroutine(SceneShowingDelay());
        }
        private IEnumerator SceneShowingDelay()
        {
            PlayerController.instance_.IsActive_ = false;
            yield return new WaitForSeconds(ParsedSceneShowingDelayProvider.GetValue());
            CutsceneManager.inst_.AddCutscene(new CutsceneManager.Cutscene(CutsceneIndex, CutsceneIcon, CutsceeSelectedIcon, Sprites, PlayedMusic));
            PlayerController.instance_.IsActive_ = true;
            Destroy(this);
        }
    }
}