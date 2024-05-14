
using MuonhoryoLibrary;

namespace Chalk
{
    public sealed class BasedOnGlobalConstParam_Float: BasedOnGlobalConstParam<float,CompositeFloat> 
    {
        protected override CompositeFloat InitializeParam(float defaultValue) =>
            new CompositeFloat(defaultValue);
    }
}