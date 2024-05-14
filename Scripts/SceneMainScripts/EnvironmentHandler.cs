

using System;
using UnityEngine;

namespace Chalk
{
    public sealed class EnvironmentHandler : MonoBehaviour 
    {
        public static EnvironmentHandler Instance_ { get; private set; }

        [SerializeField] private Transform Environment;

        public Transform Environment_=>Environment;

        private void Awake()
        {
            if (Instance_ != null)
                throw new Exception("Already have EnvironmentHandler in scene.");
            Instance_ = this;
        }
    }
}