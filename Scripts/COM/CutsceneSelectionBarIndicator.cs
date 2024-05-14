
using System;
using Chalk.Cutscenes;
using UnityEngine;
using UnityEngine.UI;

namespace Chalk
{
    public sealed class CutsceneSelectionBarIndicator : MonoBehaviour
    {
        [SerializeField] private CutsceneSelectionBar SelectionBar;
        [SerializeField] private Image Indicator;
        [SerializeField] private Sprite OnActiveSprite;
        [SerializeField] private Sprite OnInactiveSprite;

        private void Awake()
        {
            if (SelectionBar == null)
                throw new NullReferenceException("Missing SelectionBar.");
            if (Indicator== null)
                throw new NullReferenceException("Missing Indicator.");
            if (OnActiveSprite== null)
                throw new NullReferenceException("Missing OnActiveSprite.");
            if (OnInactiveSprite == null)
                throw new NullReferenceException("Missing OnInactiveSprite.");

            SelectionBar.TurnOnInteractionEvent += OnActivation;
            SelectionBar.TurnOffInteractionEvent += OnDeactivation;
        }
        private void OnActivation()
        {
            Indicator.sprite = OnActiveSprite;
        }
        private void OnDeactivation()
        {
            Indicator.sprite = OnInactiveSprite;
        }
    }
}
