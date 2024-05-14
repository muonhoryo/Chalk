


using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chalk.Cutscenes
{
    public sealed class CutsceneSelection : MonoBehaviour 
    {
        [SerializeField] private Button Button;
        [SerializeField] private Image Image;

        private int CutsceneIndex=-1;

        public void Initialize(CutsceneManager.Cutscene cutscene)
        {
            Image.sprite = cutscene.Icon;

            SpriteState state = new SpriteState();
            state.highlightedSprite = cutscene.SelectedIcon;
            Button.spriteState = state;

            CutsceneIndex = cutscene.Index;
        }
        public void OnClick()
        {
            if (CutsceneIndex < 0)
                throw new Exception("CutsceneSelection didn't initialized.");

            CutscenesShower.inst_.ShowScene(CutsceneIndex);
        }

        public void SetInteractable(bool interactable)=>
            Button.interactable=interactable;
    }
}