using MuonhoryoLibrary.Unity.COM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk
{
    public abstract class ConstProvider<TValueType> : MonoBehaviour,IConstProvider<TValueType>
    {
        [SerializeField] private TValueType Value;

        public TValueType GetValue() => Value;
    }
}
