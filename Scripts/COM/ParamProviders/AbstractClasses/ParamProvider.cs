using MuonhoryoLibrary.Unity.COM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk
{
    public abstract class ParamProvider<TParamType> : MonoBehaviour,IConstProvider<TParamType>
    {
        public event Action<TParamType> ChangeParamEvent = delegate { };

        [SerializeField] private TParamType Value;

        public TParamType Value_ 
        {
            get => Value;
            set
            {
                if (!Value.Equals(value))
                {
                    Value = value;
                    ChangeParamEvent(value);
                }
            }
        }

        TParamType IConstProvider<TParamType>.GetValue() => Value;
    }
}
