using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk
{
    public sealed class Trashcan : StaticAnimatedObj
    {
        protected override void InteractionAction()
        {
            TurnOffInteraction();
        }

        public void AnimationDone_Trashcan()
        {
            TurnOnInteraction();
        }
    }
}
