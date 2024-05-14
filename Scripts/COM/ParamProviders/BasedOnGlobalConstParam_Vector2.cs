using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity;
using UnityEngine;

namespace Chalk
{
    public sealed class BasedOnGlobalConstParam_Vector2 : BasedOnGlobalConstParam<Vector2,CompositeVector2>
    {
        protected override CompositeVector2 InitializeParam(Vector2 defaultValue) =>
            new CompositeVector2(defaultValue);
    }
}
