using System;
using System.Collections;
using System.Collections.Generic;
using Chalk.Player;
using UnityEngine;

namespace Chalk
{
    public sealed class Pen : PickUpedItem
    {
        public override event Action InteractionEvent=delegate { };
        public override event Action InteractionDoneEvent=delegate { };

        [SerializeField] private GameObject PillarPrefab;
        [SerializeField] private CameraTargetFollowingRotationModule FollowingModule;
        [SerializeField] private GameObject SoundPlayerPrefab;
        [SerializeField] private AudioClip Sound;

        private bool IsThrown = false;

        protected override void AwakeAction()
        {
            if (PillarPrefab == null)
                throw new NullReferenceException("Missing PillarPrefab.");
            if (FollowingModule == null)
                throw new NullReferenceException("Missing FollowingModule.");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsThrown)
            {
                if (collision.collider.GetComponentInChildren<PlayerController>() == null)
                {
                    IsThrown = false;
                    Pillar pill = GameObject.Instantiate(PillarPrefab).GetComponent<Pillar>();
                    if (pill == null)
                        throw new NullReferenceException("Missing Pillar.");
                    pill.transform.position=new Vector3(transform.position.x,pill.transform.position.y,transform.position.z);

                    FollowingModule.SetTarget(pill.FollowingOrigin_);

                    void InteractionDone()
                    {
                        FollowingModule.IsActive_ = false;
                        PlayerController.instance_.IsActive_ = true;
                        pill.GrowingDoneEvent -= InteractionDone;
                        InteractionDoneEvent();
                    }
                    pill.GrowingDoneEvent += InteractionDone;

                    var soundPlayer = GameObject.Instantiate(SoundPlayerPrefab).GetComponent<OneShotSoundPlayer>();
                    soundPlayer.transform.position = transform.position;
                    soundPlayer.transform.parent = transform;
                    soundPlayer.PlaySound(Sound);
                }
            }
        }

        protected override void ItemInteractAction()
        {
            Drop();
        }
        public override void Drop()
        {
            base.Drop();
            PlayerController.instance_.IsActive_ = false;
            FollowingModule.SetTarget(transform);
            FollowingModule.IsActive_ = true;
            IsThrown = true;
            InteractionEvent();
        }
    }
}
