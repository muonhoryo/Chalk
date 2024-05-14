

using UnityEngine;

namespace Chalk
{
    public sealed class RandomValue_Float : RandomValue<float>
    {
        public override float GetValue()=>
            Random.Range(ParsedMinProvider.GetValue(), ParsedMaxProvider.GetValue());
    }
}
