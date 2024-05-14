using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuonhoryoLibrary.Unity.COM;
using MuonhoryoLibrary;

namespace Chalk
{
    public abstract class BasedOnGlobalConstParam<TParamType,TCompositeParamType> : MonoBehaviour,IConstProvider<TParamType>
        where TCompositeParamType:CompositeParameter<TParamType>
    {
        [SerializeField] private string ConstName;
        
        public TCompositeParamType Param_ { get; private set; }

        private void Awake()
        {
            if (string.IsNullOrEmpty(ConstName))
                throw new System.NullReferenceException("Missing ConstName.");

            TParamType defaultValue=(TParamType)typeof(GlobalConsts.Consts).GetField(ConstName).GetValue(GlobalConsts.inst_);
            Param_ = InitializeParam(defaultValue);
        }
        protected abstract TCompositeParamType InitializeParam(TParamType defaultValue);

        public TParamType GetValue() => (TParamType)Param_;
    }
}
