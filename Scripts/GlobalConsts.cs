
using System;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public static class GlobalConsts 
    {
        public struct Consts 
        {
            public int CameraTargetMovingStepsToFullFollowing;
            public int MaxPillarsInScene;

            public float PlayerMovingSpeed;
            public float CameraBottomLimit;
            public float CameraTopLimit;
            public float MovingAddForceModifier;
            public float InteractionSphereCastRadius;
            public float InteractionMaxDistance;
            public float BreadThrowingForce;
            public float FlashlightInteractionDelay;
            public float PillarMinGrowingHeight;
            public float PillarMaxGrowingHeight;
            public float PillarMinLength;
            public float PillarMaxLength;
            public float PillarMinWidth;
            public float PillarMaxWidth;
            public float PillarGrowingStep;
            public float BreadRespawnCheckHeight;
            public float CutsceneImageShowingTime;
            public float UICreateButtonDelay;
            public float EndGameDistance;
            public float MusicTransitionSpeed;

            public Vector2 BreadThrowingDispersion;
            public Vector2 BreadRespawnArea;

            public Vector3 CameraDefaultRotationOffset;
            public Vector3 CameraLocalPositionOffset;
            public Vector3 CameraGlobalPositionOffset;
            public Vector3 BreadHandlingPositionOffset;
            public Vector3 BreadHandlingRotationOffset;
            public Vector3 CupHandlingPositionOffset;
            public Vector3 CupHandlingRotationOffset;
            public Vector3 FlashlightHandlingPositionOffset;
            public Vector3 FlashlightHandlingRotationOffset;
            public Vector3 PenHandlingPositionOffset;
            public Vector3 PenHandlingRotationOffset;

            public RotationInverter.InversionMode InversionMode;
            public RotationInverter.InversionMode InversionOnCardInteractMode;

            public Consts(
                int CameraTargetMovingStepsToFullFollowing,
                int MaxPillarsInScene,
                float PlayerMovingSpeed,
                float CameraBottomLimit,
                float CameraTopLimit,
                float MovingAddForceModifier,
                float InteractionSphereCastRadius,
                float InteractionMaxDistance,
                float BreadThrowingForce,
                float FlashlightInteractionDelay,
                float PillarMinGrowingHeight,
                float PillarMaxGrowingHeight,
                float PillarMinLength,
                float PillarMaxLength,
                float PillarMinWidth,
                float PillarMaxWidth,
                float PillarGrowingStep,
                float BreadRespawnCheckHeight,
                float CutsceneImageShowingTime,
                float UICreateButtonDelay,
                float EndGameDistance,
                float MusicTransitionSpeed,
                Vector2 BreadThrowingDispersion,
                Vector2 BreadRespawnArea,
                Vector3 CameraDefaultRotationOffset,
                Vector3 CameraLocalPositionOffset,
                Vector3 CameraGlobalPositionOffset,
                Vector3 BreadHandlingPositionOffset,
                Vector3 BreadHandlingRotationOffset,
                Vector3 CupHandlingPositionOffset,
                Vector3 CupHandlingRotationOffset,
                Vector3 FlashlightHandlingPositionOffset,
                Vector3 FlashlightHandlingRotationOffset,
                Vector3 PenHandlingPositionOffset,
                Vector3 PenHandlingRotationOffset,
                RotationInverter.InversionMode InversionMode,
                RotationInverter.InversionMode InversionOnCardInteractMode)
            {
                this.CameraTargetMovingStepsToFullFollowing = CameraTargetMovingStepsToFullFollowing;
                this.MaxPillarsInScene = MaxPillarsInScene;

                this.PlayerMovingSpeed = PlayerMovingSpeed;
                this.CameraBottomLimit = CameraBottomLimit;
                this.CameraTopLimit = CameraTopLimit;
                this.MovingAddForceModifier = MovingAddForceModifier;
                this.InteractionSphereCastRadius = InteractionSphereCastRadius;
                this.InteractionMaxDistance = InteractionMaxDistance;
                this.BreadThrowingForce = BreadThrowingForce;
                this.FlashlightInteractionDelay = FlashlightInteractionDelay;
                this.PillarMinGrowingHeight = PillarMinGrowingHeight;
                this.PillarMaxGrowingHeight = PillarMaxGrowingHeight;
                this.PillarMinLength = PillarMinLength;
                this.PillarMaxLength = PillarMaxLength;
                this.PillarMinWidth = PillarMinWidth;
                this.PillarMaxWidth = PillarMaxWidth;
                this.PillarGrowingStep = PillarGrowingStep;
                this.BreadRespawnCheckHeight = BreadRespawnCheckHeight;
                this.CutsceneImageShowingTime = CutsceneImageShowingTime;
                this.UICreateButtonDelay = UICreateButtonDelay;
                this.EndGameDistance = EndGameDistance;
                this.MusicTransitionSpeed = MusicTransitionSpeed;

                this.BreadThrowingDispersion = BreadThrowingDispersion;
                this.BreadRespawnArea = BreadRespawnArea;

                this.CameraDefaultRotationOffset = CameraDefaultRotationOffset;
                this.CameraLocalPositionOffset = CameraLocalPositionOffset;
                this.CameraGlobalPositionOffset = CameraGlobalPositionOffset;
                this.BreadHandlingPositionOffset = BreadHandlingPositionOffset;
                this.BreadHandlingRotationOffset = BreadHandlingRotationOffset;
                this.CupHandlingPositionOffset = CupHandlingPositionOffset;
                this.CupHandlingRotationOffset = CupHandlingRotationOffset;
                this.FlashlightHandlingPositionOffset = FlashlightHandlingPositionOffset;
                this.FlashlightHandlingRotationOffset = FlashlightHandlingRotationOffset;
                this.PenHandlingPositionOffset = PenHandlingPositionOffset;
                this.PenHandlingRotationOffset = PenHandlingRotationOffset;

                this.InversionMode = InversionMode;
                this.InversionOnCardInteractMode = InversionOnCardInteractMode;
            }
        }
        public static event Action ConstsChangedEvent = delegate { };

        public static Consts inst_ { get; private set; }

        public static void Initialize(Consts inst)
        {
            inst_ = inst;
            ConstsChangedEvent();
        }
    }
}
