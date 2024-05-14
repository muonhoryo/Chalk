using System;
using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public sealed class ItemDroppingInputHandler : MonoBehaviour,IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private string InputName_Drop;
        [SerializeField] private bool IsActiveOnAwake = false;

        [SerializeField] private MonoBehaviour InteractionModule;

        private IPickUpModule ParsedInteractionModule;

        private bool IsActive = false;

        public bool IsActive_
        {
            get => IsActive;
            set
            {
                if (IsActive != value)
                {
                    IsActive = value;
                    enabled = value;
                    ParsedInteractionModule.UnparsedModuleActivityChanging(value);
                    if (IsActive)
                        ActivateModuleEvent();
                    else
                        DeactivateModuleEvent();
                }
            }
        }

        private void OnEnable()
        {
            if (!IsActive_)
                enabled = false;
        }
        private void OnDisable()
        {
            if (IsActive_)
                enabled = true;
        }
        private void Awake()
        {
            ParsedInteractionModule = InteractionModule as IPickUpModule;
            if (ParsedInteractionModule == null)
                throw new NullReferenceException("Missing InteractionModule.");

            if (!IsActiveOnAwake)
                enabled = false;
        }
        private void Start()
        {
            if (IsActive_ != enabled)
                IsActive_ = true;
        }
        private void Update()
        {
            if (Input.GetButtonDown(InputName_Drop))
            {
                ParsedInteractionModule.DropItem();
            }
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
