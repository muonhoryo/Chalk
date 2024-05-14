
using System;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public abstract class RandomValue<TParamType> : MonoBehaviour,IConstProvider<TParamType>
    {
        [SerializeField] private MonoBehaviour MinProvider;
        [SerializeField] private MonoBehaviour MaxProvider;

        protected IConstProvider<TParamType> ParsedMinProvider;
        protected IConstProvider<TParamType> ParsedMaxProvider;

        private void Awake()
        {
            ParsedMinProvider = MinProvider as IConstProvider<TParamType>;
            if (ParsedMinProvider == null)
                throw new NullReferenceException("Missing MinProvider.");

            ParsedMaxProvider=MaxProvider as IConstProvider<TParamType>;
            if (ParsedMaxProvider == null)
                throw new NullReferenceException("Missing MaxProvider.");
        }

        public abstract TParamType GetValue();
    }
}