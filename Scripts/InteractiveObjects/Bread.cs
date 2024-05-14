
using Chalk.Player;
using MuonhoryoLibrary.Unity;
using MuonhoryoLibrary.Unity.COM;
using System;
using UnityEngine;

namespace Chalk
{
    public sealed class Bread :PickUpedItem
    {
        public override event Action InteractionDoneEvent = delegate { };
        public override event Action InteractionEvent = delegate { };

        [SerializeField] private GameObject BreakingEffect;
        [SerializeField] private MeshRenderer Mesh;
        [SerializeField] private CameraTargetFollowingRotationModule FollowingModule;
        [SerializeField] private int RespawnPositionCheckLayerMask;

        [SerializeField] private MonoBehaviour ThrowingDispersionProvider;
        [SerializeField] private MonoBehaviour ThrowingForceProvider;
        [SerializeField] private MonoBehaviour CameraViewModule;
        [SerializeField] private MonoBehaviour RespawnAreaProvider;
        [SerializeField] private MonoBehaviour RespawnPositionCheckHeight;
        [SerializeField] private MonoBehaviour InteractionModule;

        private IConstProvider<Vector2> ParsedThrowingDispersionProvider;
        private IConstProvider<float> ParsedThrowingForceProvider;
        private ICameraViewRotationModule ParsedCameraViewModule;
        private IConstProvider<Vector2> ParsedRespawnAreaProvider;
        private IConstProvider<float> ParsedRespawnPositionCheckHeight;
        private IInteractionModule ParsedInteractionModule;

        private bool IsThrown = false;

        protected override void AwakeAction()
        {
            ParsedThrowingDispersionProvider = ThrowingDispersionProvider as IConstProvider<Vector2>;
            if (ParsedThrowingDispersionProvider == null)
                throw new NullReferenceException("Missing ThrowingDispersionProvider.");

            ParsedThrowingForceProvider = ThrowingForceProvider as IConstProvider<float>;
            if (ParsedThrowingForceProvider == null)
                throw new NullReferenceException("Missing ThrowingForceProvider.");

            ParsedCameraViewModule = CameraViewModule as ICameraViewRotationModule;
            if (ParsedCameraViewModule == null)
                throw new NullReferenceException("Missing CameraViewModule.");

            ParsedRespawnAreaProvider = RespawnAreaProvider as IConstProvider<Vector2>;
            if (ParsedRespawnAreaProvider == null)
                throw new NullReferenceException("Missing RespawnAreaProvider.");

            ParsedRespawnPositionCheckHeight= RespawnPositionCheckHeight as IConstProvider<float>;
            if (ParsedRespawnPositionCheckHeight == null)
                throw new NullReferenceException("Missing RespawnPositionCheckHeight.");

            ParsedInteractionModule = InteractionModule as IInteractionModule;
            if (ParsedInteractionModule == null)
                throw new NullReferenceException("Missing InteractionModule.");

            if (BreakingEffect == null)
                throw new NullReferenceException("Missing BreakingEffect.");
            if (Mesh == null)
                throw new NullReferenceException("Missing Mesh.");
            if (FollowingModule == null)
                throw new NullReferenceException("Missing FollowingModule.");
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (IsThrown&&collision.gameObject.GetComponentInChildren<PlayerController>()==null)
            {
                Mesh.enabled = false;
                GameObject.Instantiate(BreakingEffect, transform.position, transform.rotation, EnvironmentHandler.Instance_.Environment_);
                IsThrown = false;
                TurnPhysics(false);
                PlayerController.instance_.IsActive_ = true;
                FollowingModule.IsActive_ = false;
                InteractionDoneEvent();
            }
        }

        protected override void ItemInteractAction()
        {
            PlayerController.instance_.IsActive_ = false;
            Drop();
            FollowingModule.SetTarget(transform);
            FollowingModule.IsActive_ = true;
            Vector3 cameraRotation = ParsedCameraViewModule.CurrentViewDirection_.StereoRotationFromDirection();
            Vector2 disp = ParsedThrowingDispersionProvider.GetValue() * 0.5f;
            cameraRotation += new Vector3
                (UnityEngine.Random.Range(-disp.x, disp.x),
                UnityEngine.Random.Range(-disp.y, disp.y),
                0);
            Vector3 throwingDirection = cameraRotation.DirectionFromStereoRotation();
            rgbody.AddForce(throwingDirection * ParsedThrowingForceProvider.GetValue(), ForceMode.Impulse);
            IsThrown = true;
            ParsedPickUpModule.PickUpItemEvent += OnOtherItemPickUp;
            ParsedInteractionModule.InteractionEvent += OnOtherItemInteract_InteractionModule;

            InteractionEvent();
        }
        private void OnOtherItemInteract_InteractionModule(IInteractiveObject i)
        {
            if(i is not IPickUpedItem)
            {
                OnOtherItemInteract_PickUpModule(i);
            }
        } 
        private void OnOtherItemInteract_PickUpModule(IInteractiveObject i)
        {
            ParsedPickUpModule.InteractionEvent -= OnOtherItemInteract_PickUpModule;
            ParsedInteractionModule.InteractionEvent -= OnOtherItemInteract_InteractionModule ;
            ParsedPickUpModule.PickUpItemEvent -= OnOtherItemPickUp;
            TurnPhysics(true);
            Mesh.enabled = true;
            RandomMove();
        }
        private void OnOtherItemPickUp(IPickUpedItem i)
        {
            ParsedPickUpModule.InteractionEvent += OnOtherItemInteract_PickUpModule;
            ParsedPickUpModule.PickUpItemEvent -= OnOtherItemPickUp;
        }

        private void RandomMove()
        {
            Vector2 area=ParsedRespawnAreaProvider.GetValue();
            Vector3 pos = new Vector3
                (UnityEngine.Random.Range(-area.x / 2, area.x / 2),
                ParsedRespawnPositionCheckHeight.GetValue(),
                UnityEngine.Random.Range(-area.y / 2, area.y / 2));
            RaycastHit hit;
            Physics.Raycast(pos, Vector3.down,out hit, 10000, RespawnPositionCheckLayerMask);
            transform.position= hit.point;
        }
    }
}
