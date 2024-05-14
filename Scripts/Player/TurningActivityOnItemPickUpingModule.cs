

using System;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Player
{
    public sealed class TurningActivityOnItemPickUpingModule : MonoBehaviour
    {
        [SerializeField] private bool IsTurnOnOnPicking = true;

        [SerializeField] private MonoBehaviour PickUpModule;
        [SerializeField] private MonoBehaviour[] TargetModules;

        private IPickUpModule ParsedPickUpModule;
        private IActiveModule[] ParsedTargetModules;

        private void Awake()
        {
            ParsedPickUpModule = PickUpModule as IPickUpModule;
            if (ParsedPickUpModule == null)
                throw new NullReferenceException("Missing PickUpModule.");

            if (TargetModules == null || TargetModules.Length == 0)
                throw new NullReferenceException("Missing TargetModules.");

            ParsedTargetModules=new IActiveModule[TargetModules.Length];
            for(int i = 0; i < TargetModules.Length; i++)
            {
                ParsedTargetModules[i] = TargetModules[i] as IActiveModule;
                if (ParsedTargetModules[i] == null)
                    throw new NullReferenceException("Missing target module at index " + i);
            }

            foreach(var mod in ParsedTargetModules)
            {
                void OnPickUp_Active(IPickUpedItem i)
                {
                    mod.IsActive = true;
                    mod.DeactivateModuleEvent += OnDeactivation;
                }
                void OnDrop_Active(IPickUpedItem i)
                {
                    mod.DeactivateModuleEvent -= OnDeactivation;
                    mod.IsActive = false;
                }
                void OnDeactivation()
                {
                    mod.IsActive = true;
                }

                void OnPickUp_Deactive(IPickUpedItem i)
                {
                    mod.IsActive = false;
                    mod.ActivateModuleEvent += OnActivation;
                }
                void OnDrop_Deactive(IPickUpedItem i)
                {
                    mod.ActivateModuleEvent -= OnActivation;
                    mod.IsActive = true;
                }
                void OnActivation()
                {
                    mod.IsActive = false;
                }

                if (IsTurnOnOnPicking)
                {
                    ParsedPickUpModule.PickUpItemEvent += OnPickUp_Active;
                    ParsedPickUpModule.DropItemEvent += OnDrop_Active;
                }
                else
                {
                    ParsedPickUpModule.PickUpItemEvent += OnPickUp_Deactive;
                    ParsedPickUpModule.DropItemEvent += OnDrop_Deactive;
                }
            }
        }
    }
}