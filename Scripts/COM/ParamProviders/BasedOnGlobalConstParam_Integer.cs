
using MuonhoryoLibrary;
using UnityEngine;

namespace Chalk
{
    public sealed class BasedOnGlobalConstParam_Integer : BasedOnGlobalConstParam<int,CompositeInteger>
    {
        protected override CompositeInteger InitializeParam(int defaultValue)=>
            new CompositeInteger(defaultValue);
    }
}

