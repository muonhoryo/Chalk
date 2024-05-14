

using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public abstract class GlobalConstProvider<TConstType> : MonoBehaviour,IConstProvider<TConstType>
    {
        [SerializeField] private string GlobalConstName;
        public TConstType Value_ { get; private set; }

        private void Awake()
        {
            Value_ = (TConstType)typeof(GlobalConsts.Consts).GetField(GlobalConstName).GetValue(GlobalConsts.inst_);
        }

        TConstType IConstProvider<TConstType>.GetValue() => Value_;
    }
}
