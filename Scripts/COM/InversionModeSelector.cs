
using MuonhoryoLibrary.Unity.COM;

namespace Chalk
{
    public sealed class InversionModeSelector : ModuleSelector<IConstProvider<RotationInverter.InversionMode>>,
        IConstProvider<RotationInverter.InversionMode>
    {
        protected override void SubscribeOnModulesEvents(IConstProvider<RotationInverter.InversionMode> module) { }
        protected override void UnsubscribeFromModuleEvents(IConstProvider<RotationInverter.InversionMode> module) { }

        public RotationInverter.InversionMode GetValue() => 
            CurrentModule_ != null ? CurrentModule_.GetValue() : RotationInverter.InversionMode.None;
    }
}
