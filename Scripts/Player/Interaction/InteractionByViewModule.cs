


using MuonhoryoLibrary.Unity.COM;
using System;
using UnityEngine;

namespace Chalk.Player
{
    public sealed class InteractionByViewModule : MonoBehaviour, IInteractionModule, IActiveModule
    {
        public event Action<IInteractiveObject> InteractiveObjHasBeenFoundEvent = delegate { };
        public event Action InteractiveObjHasBeenLostEvent = delegate { };
        public event Action<IInteractiveObject> InteractionEvent = delegate { };
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private Transform SphereCastDataOrigin;
        [SerializeField] private int LayerMask;
        [SerializeField] private bool IsActiveOnAwake = false;

        [SerializeField] private MonoBehaviour SphereCastRadiusProvider;
        [SerializeField] private MonoBehaviour SphereCastMaxDistanceProvider;
        [SerializeField] private MonoBehaviour CameraViewModule;

        private IConstProvider<float> ParsedSphereCastRadiusProvider;
        private IConstProvider<float> ParsedSphereCastMaxDistProvider;
        private ICameraViewRotationModule ParsedCameraViewModule;

        public  IInteractiveObject InteractiveObject_ { get; private set; }

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
                    ParsedSphereCastRadiusProvider.UnparsedModuleActivityChanging(value);
                    ParsedSphereCastMaxDistProvider.UnparsedModuleActivityChanging(value);
                    if (IsActive)
                        ActivateModuleEvent();
                    else
                        DeactivateModuleEvent();
                }
            }
        }
        public float SphereCastRadius_ => ParsedSphereCastRadiusProvider.GetValue();
        public float SphereCastMaxDistance_=> ParsedSphereCastMaxDistProvider.GetValue();

        private void Awake()
        {
            if (SphereCastDataOrigin == null)
                throw new NullReferenceException("Missing SphereCastDataOrigin.");

            ParsedSphereCastRadiusProvider = SphereCastRadiusProvider as IConstProvider<float>;
            if (ParsedSphereCastRadiusProvider == null)
                throw new NullReferenceException("Missing SphereCastRadiusProvider.");

            ParsedSphereCastMaxDistProvider = SphereCastMaxDistanceProvider as IConstProvider<float>;
            if (ParsedSphereCastMaxDistProvider == null)
                throw new NullReferenceException("Missing SphereCastMaxDistanceProvider.");

            ParsedCameraViewModule = CameraViewModule as ICameraViewRotationModule;
            if (ParsedCameraViewModule == null)
                throw new NullReferenceException("Missing CameraViewModule.");

            if (!IsActiveOnAwake)
                enabled = false;
        }
        private void Start()
        {
            if (enabled != IsActive)
                IsActive_ = true;
        }
        private void Update()
        {
            IInteractiveObject obj = null;
            RaycastHit[] hitsInfo = Physics.SphereCastAll
                (origin: SphereCastDataOrigin.position,
                radius: SphereCastRadius_,
                direction: ParsedCameraViewModule.CurrentViewDirection_,
                maxDistance: SphereCastMaxDistance_,
                layerMask: LayerMask,
                queryTriggerInteraction: QueryTriggerInteraction.Collide);
            foreach (var hit in hitsInfo)
            {
                obj = hit.collider.gameObject.GetComponentInParent<IInteractiveObject>();
                if (obj != null && obj.CanInteract_)
                    break;
            }
            if (obj != null)
            {
                InteractiveObject_ = obj;
                InteractiveObjHasBeenFoundEvent(InteractiveObject_);
            }
            else if (InteractiveObject_ != null)
            {
                InteractiveObject_ = null;
                InteractiveObjHasBeenLostEvent();
            }
        }

        public void Interact()
        {
            if (InteractiveObject_ != null)
            {
                InteractiveObject_.Interact();
                InteractionEvent(InteractiveObject_);
            }
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}