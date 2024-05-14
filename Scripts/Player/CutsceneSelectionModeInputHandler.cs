


using System;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;
using Chalk.Cutscenes;
using System.Security.Cryptography;

namespace Chalk.Player
{
    public sealed class CutsceneSelectionModeInputHandler : MonoBehaviour,IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private string InputName_TurnSceneSelectionMode;
        [SerializeField] private bool IsActiveOnAwake = false;
        [SerializeField] private CutsceneSelectionBar SelectionBar;

        private bool IsSelectionModeTurnOn = false;
        private bool IsInitialized = false;
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
                    if (IsActive_)
                        ActivateModuleEvent();
                    else
                        DeactivateModuleEvent();
                }
            }
        }

        private void Awake()
        {
            if (string.IsNullOrEmpty(InputName_TurnSceneSelectionMode))
                throw new NullReferenceException("Missing input name - TurnSceneSelectionMode.");
            if (SelectionBar == null)
                throw new NullReferenceException("Missing SelectionBar.");

            if (!IsActiveOnAwake)
            {
                IsInitialized = true;
                enabled = false;
            }
        }
        private void OnEnable()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                IsActive_ = true;
            }

            if (!IsActive_)
                enabled = false;
        }
        private void OnDisable()
        {
            if (IsActive_)
                enabled = true;
        }

        private void Update()
        {
            if (Input.GetButtonDown(InputName_TurnSceneSelectionMode))
                TurnSelectionMode();
        }

        private void TurnSelectionMode()
        {
            if(IsSelectionModeTurnOn)
            {
                PlayerController.instance_.IsActive_ = true;
                Cursor.visible = false;
                IsSelectionModeTurnOn = false;
                CutscenesShower.inst_.StartShowingSceneEvent -= OnShowCutscene;
                SelectionBar.TurnOffInteraction();
            }
            else
            {
                PlayerController.instance_.IsActive_ = false;
                IsActive_ = true;
                Cursor.visible = true;
                IsSelectionModeTurnOn = true;
                CutscenesShower.inst_.StartShowingSceneEvent += OnShowCutscene;
                SelectionBar.TurnOnInteraction();
            }
        }
        private void OnShowCutscene(CutsceneManager.Cutscene i)
        {
            CutscenesShower.inst_.StartShowingSceneEvent -= OnShowCutscene;
            Cursor.visible = false;
            IsSelectionModeTurnOn = false;
            IsActive = false;
            SelectionBar.TurnOffInteraction();
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}