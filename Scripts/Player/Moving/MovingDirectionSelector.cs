using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Player
{
    public sealed class MovingDirectionSelector : ModuleSelector<IMovingDirectionCalculator>,IMovingDirectionCalculator
    {
        protected override void SubscribeOnModulesEvents(IMovingDirectionCalculator module) { }
        protected override void UnsubscribeFromModuleEvents(IMovingDirectionCalculator module) { }

        public Vector3 GetDirection(Vector2 input) 
        {
            return CurrentModule_ != null ? CurrentModule_.GetDirection(input) : Vector3.zero;
        }
    }
}
