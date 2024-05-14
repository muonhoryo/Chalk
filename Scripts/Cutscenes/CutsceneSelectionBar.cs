

using System;
using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Cutscenes
{
    public sealed class CutsceneSelectionBar : MonoBehaviour
    {
        public event Action TurnOnInteractionEvent = delegate { };
        public event Action TurnOffInteractionEvent = delegate { };

        [SerializeField] private float Offset;
        [SerializeField] private GameObject ButtonPrefab;
        [SerializeField] private Transform ButtonsBar;

        [SerializeField] private MonoBehaviour CreateButtonDelayProvider;

        private IConstProvider<float> ParsedCreateButtonDelayProvider;

        private List<CutsceneSelection>ShowedButtons=new List<CutsceneSelection>();
        private bool IsInteractable = false;

        private void Awake()
        {
            if (ButtonPrefab == null)
                throw new NullReferenceException("Missing ButtonPrefab.");
            if (ButtonsBar == null)
                throw new NullReferenceException("Missing ButtonsBar.");

            ParsedCreateButtonDelayProvider = CreateButtonDelayProvider as IConstProvider<float>;
            if (ParsedCreateButtonDelayProvider == null)
                throw new NullReferenceException("Missing CreateButtonDelayProvider.");

            CutsceneManager.inst_.UpdateCutscenesListEvent += (i) => ShowButtons(); 
        }

        private void ShowButtons()
        {
            StopAllCoroutines();
            foreach (var button in ShowedButtons)
                if(button!=null)
                    Destroy(button.gameObject);
            StartCoroutine(DelayedCreateButtons());
        }
        private IEnumerator DelayedCreateButtons()
        {
            var showedScenes = CutsceneManager.inst_.GetShowedScenes();
            ShowedButtons = new List<CutsceneSelection>(showedScenes.Length);
            for (int i = 0; i < showedScenes.Length; i++)
            {
                yield return new WaitForSeconds(ParsedCreateButtonDelayProvider.GetValue());

                GameObject buttonObj = GameObject.Instantiate(ButtonPrefab, ButtonsBar);
                buttonObj.transform.localPosition = new Vector3(i * Offset, 0, 0);

                CutsceneSelection button = buttonObj.GetComponent<CutsceneSelection>();
                button.Initialize(showedScenes[i]);

                ShowedButtons.Add(button);
            }
        }

        public void TurnOnInteraction()
        {
            if (!IsInteractable)
            {
                foreach (var button in ShowedButtons)
                    button.SetInteractable(true);
                IsInteractable = true;
                TurnOnInteractionEvent();
            }
        }
        public void TurnOffInteraction()
        {
            if (IsInteractable)
            {
                foreach (var button in ShowedButtons)
                    button.SetInteractable(false);
                IsInteractable = false;
                TurnOffInteractionEvent();
            }
        }
    }
}