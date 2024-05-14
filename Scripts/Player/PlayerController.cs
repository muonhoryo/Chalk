
using System;
using MuonhoryoLibrary.Unity;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Player
{
    public sealed class PlayerController : MonoBehaviour,IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        public static PlayerController instance_ { get; private set; }

        [SerializeField] private MonoBehaviour[] Submodules;

        [SerializeField] private bool IsActiveOnAwake = false;

        private bool IsActive = false;

        public bool IsActive_
        {
            get => IsActive;
            set
            {
                if (IsActive_ != value)
                {
                    IsActive = value;
                    enabled = value;
                    foreach (var module in Submodules)
                        module.UnparsedModuleActivityChanging(value);
                    if (IsActive)
                        ActivateModuleEvent();
                    else
                        DeactivateModuleEvent();
                }
            }
        }

        private void Awake()
        {
            if (Submodules == null || Submodules.Length == 0)
                throw new NullReferenceException("Missing Submodules.");

            foreach (var mod in Submodules)
                if (mod == null)
                    throw new NullReferenceException("Missing submodule.");

            if (instance_ != null)
                throw new Exception("Already have PlayerController in scene.");
            instance_ = this;

            if (!IsActiveOnAwake)
                enabled = false;
        }
        private void Start()
        {
            if (enabled != IsActive)
                IsActive_ = true;
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
