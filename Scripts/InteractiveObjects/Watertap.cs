using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk
{
    public sealed class Watertap : StaticAnimatedObj
    {
        [SerializeField] private InversionModeSelector InversionModeProvider;
        [SerializeField] private int IsInvertedIndex;
        [SerializeField] private int IsNormalIndex;

        public bool IsInverted_ { get; private set; } = false;

        protected override void AwakeAction()
        {
            if (InversionModeProvider == null)
                throw new NullReferenceException("Missing Rotationinverter.");
        }
        protected override void InteractionAction()
        {
            IsInverted_ = !IsInverted_;
            InversionModeProvider.SelectModule(
                InversionModeProvider.CurrentModuleIndex_ == IsInvertedIndex ?
                IsNormalIndex : IsInvertedIndex);
            TurnOffInteraction();
        }
        protected override void AnimationDoneAction()
        {
            TurnOnInteraction();
        }

    }
}
