using MuonhoryoLibrary.Unity;
using MuonhoryoLibrary.Unity.COM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk
{
    public sealed class CameraTargetFollowingRotationModule : MonoBehaviour,IActiveModule
    {

        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };
        public event Action StartFullFollowingEvent = delegate { };
        public event Action<Transform> ChangeTargetEvent = delegate { };

        [SerializeField] private Transform Target;
        [SerializeField] private Transform Camera;

        [SerializeField] private MonoBehaviour StepToFullFollowingProvider;
        [SerializeField] private MonoBehaviour CameraViewModule;

        private IConstProvider<int> ParsedStepToFullFollProvider;
        private ICameraViewRotationModule ParsedCameraViewModule;

        [SerializeField] private bool IsActiveOnAwake = false;

        private int RemainingSteps = 0;
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
                    ParsedStepToFullFollProvider.UnparsedModuleActivityChanging(value);
                    ParsedCameraViewModule.UnparsedModuleActivityChanging(value);
                    if (IsActive)
                    {
                        RemainingSteps = ParsedStepToFullFollProvider.GetValue();
                        ActivateModuleEvent();
                    }
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
            ParsedStepToFullFollProvider = StepToFullFollowingProvider as IConstProvider<int>;
            if (ParsedStepToFullFollProvider == null)
                throw new NullReferenceException("Missing StepToFullFollowingProvider.");

            ParsedCameraViewModule = CameraViewModule as ICameraViewRotationModule;
            if (ParsedCameraViewModule == null)
                throw new NullReferenceException("Missing CameraViewModule.");

            if (!IsActiveOnAwake)
                enabled = false;
        }
        private void Start()
        {
            if (IsActive_ != enabled)
                IsActive_ = true;
        }
        private void LateUpdate()
        {
            Vector3 targetRot = (Target.position - Camera.position).StereoRotationFromDirection();
            if (RemainingSteps > 0)
            {
                int divider = RemainingSteps + 1;
                float GetRotationStep(float target, float current, int div)
                {
                    float step;
                    if (current > target)
                    {
                        float diff = current - target;
                        step = diff > 180 ? 360 - diff : -diff;
                    }
                    else
                    {
                        float diff = target - current;
                        step = diff > 180 ? diff - 360 : diff;
                    }
                    return step / div;
                }
                float x, y, z;
                x = GetRotationStep(targetRot.x, Camera.eulerAngles.x, divider);
                y = GetRotationStep(targetRot.y, Camera.eulerAngles.y, divider);
                z = GetRotationStep(targetRot.z, Camera.eulerAngles.z, divider);
                ParsedCameraViewModule.SetRotation(Camera.eulerAngles + new Vector3(x, y, z));
                RemainingSteps--;
                if (RemainingSteps == 0)
                    StartFullFollowingEvent();
            }
            else
            {
                ParsedCameraViewModule.SetRotation(targetRot);
            }
        }

        public void SetTarget(Transform target)
        {
            if (target == null)
                throw new ArgumentNullException("Missing target.");

            Target = target;
            ChangeTargetEvent(target);
        }

        bool IActiveModule.IsActive { get => IsActive_;set=> IsActive_ = value; }
    }
}
