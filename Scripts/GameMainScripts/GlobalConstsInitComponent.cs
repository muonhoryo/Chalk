
using Chalk.Exceptions;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Initialization
{
    public sealed class GlobalConstsInitComponent : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField][Range(0, 1000000)] private int CameraTargetMovingStepsToFullFollowing;
        [SerializeField][Range(1, 1000)] private int MaxPillarsInScene;

        [SerializeField] [Range(0, 10000)] private float PlayerMovingSpeed;
        [SerializeField][Range(0, 360)] private float CameraBottomLimit;
        [SerializeField][Range(0, 360)] private float CameraTopLimit;
        [SerializeField][Range(0, 10000)] private float MovingAddForceModifier;
        [SerializeField][Range(0, 100)] private float InteractionSphereCastRadius;
        [SerializeField][Range(0, 1000)] private float InteractionMaxDistance;
        [SerializeField][Range(0, 10000)] private float BreadThrowingForce;
        [SerializeField][Range(0, 100)] private float FlashlightInteractionDelay;
        [SerializeField][Range(0, 100)] private float PillarMinGrowingHeight;
        [SerializeField][Range(0, 100)] private float PillarMaxGrowingHeight;
        [SerializeField][Range(0, 100)] private float PillarMinLength;
        [SerializeField][Range(0, 100)] private float PillarMaxLength;
        [SerializeField][Range(0, 100)] private float PillarMinWidth;
        [SerializeField][Range(0, 100)] private float PillarMaxWidth;
        [SerializeField][Range(0, 10)] private float PillarGrowingStep;
        [SerializeField][Range(0, 100)] private float BreadRespawnCheckHeight;
        [SerializeField][Range(0, 1000)] private float CutsceneImageShowingTime;
        [SerializeField][Range(0, 100)] private float UICreateButtonDelay;
        [SerializeField][Range(0, 10000)] private float EndGameDistance;
        [SerializeField][Range(0, 1)] private float MusicTransitionSpeed;

        [SerializeField] private Vector2 BreadThrowingDispersion;
        [SerializeField] private Vector2 BreadRespawnArea;

        [SerializeField] private Vector3 CameraDefaultRotationOffset;
        [SerializeField] private Vector3 CameraLocalPositionOffset;
        [SerializeField] private Vector3 CameraGlobalPositionOffset;
        [SerializeField] private Vector3 BreadHandlingPositionOffset;
        [SerializeField] private Vector3 BreadHandlingRotationOffset;
        [SerializeField] private Vector3 CupHandlingPositionOffset;
        [SerializeField] private Vector3 CupHandlingRotationOffset;
        [SerializeField] private Vector3 FlashlightHandlingPositionOffset;
        [SerializeField] private Vector3 FlashlightHandlingRotationOffset;
        [SerializeField] private Vector3 PenHandlingPositionOffset;
        [SerializeField] private Vector3 PenHandlingRotationOffset;

        [SerializeField] private RotationInverter.InversionMode InversionMode;
        [SerializeField] private RotationInverter.InversionMode InversionOnCardInteractMode;
#endif
        private void Awake()
        {
            ConstInitialization.Initialize();
            Destroy(this);
        }
#if UNITY_EDITOR
        [ContextMenu("WriteToFile")]
        public void WriteToFile()
        {
            ConstInitialization.WriteGlobalConsts(new GlobalConsts.Consts(
                CameraTargetMovingStepsToFullFollowing: CameraTargetMovingStepsToFullFollowing,
                MaxPillarsInScene: MaxPillarsInScene,

                PlayerMovingSpeed: PlayerMovingSpeed,
                CameraBottomLimit:CameraBottomLimit,
                CameraTopLimit:CameraTopLimit,
                MovingAddForceModifier: MovingAddForceModifier,
                InteractionSphereCastRadius: InteractionSphereCastRadius,
                InteractionMaxDistance: InteractionMaxDistance,
                BreadThrowingForce: BreadThrowingForce,
                FlashlightInteractionDelay: FlashlightInteractionDelay,
                PillarMinGrowingHeight: PillarMinGrowingHeight,
                PillarMaxGrowingHeight: PillarMaxGrowingHeight,
                PillarMinLength: PillarMinLength,
                PillarMaxLength: PillarMaxLength,
                PillarMinWidth: PillarMinWidth,
                PillarMaxWidth: PillarMaxWidth,
                PillarGrowingStep: PillarGrowingStep,
                BreadRespawnCheckHeight: BreadRespawnCheckHeight,
                CutsceneImageShowingTime: CutsceneImageShowingTime,
                UICreateButtonDelay: UICreateButtonDelay,
                EndGameDistance: EndGameDistance,
                MusicTransitionSpeed: MusicTransitionSpeed,

                BreadThrowingDispersion: BreadThrowingDispersion,
                BreadRespawnArea: BreadRespawnArea,

                CameraDefaultRotationOffset:CameraDefaultRotationOffset,
                CameraLocalPositionOffset:CameraLocalPositionOffset,
                CameraGlobalPositionOffset: CameraGlobalPositionOffset,
                BreadHandlingPositionOffset: BreadHandlingPositionOffset,
                BreadHandlingRotationOffset: BreadHandlingRotationOffset,
                CupHandlingPositionOffset: CupHandlingPositionOffset,
                CupHandlingRotationOffset: CupHandlingRotationOffset,
                FlashlightHandlingPositionOffset: FlashlightHandlingPositionOffset,
                FlashlightHandlingRotationOffset: FlashlightHandlingRotationOffset,
                PenHandlingPositionOffset: PenHandlingPositionOffset,
                PenHandlingRotationOffset: PenHandlingRotationOffset,

                InversionMode: InversionMode,
                InversionOnCardInteractMode: InversionOnCardInteractMode));
        }
#endif
    }
}
