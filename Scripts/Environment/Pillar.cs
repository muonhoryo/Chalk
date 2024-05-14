using System;
using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Collections;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public sealed class Pillar : MonoBehaviour
    {
        public event Action GrowingDoneEvent = delegate { };

        private static SingleLinkedList<Pillar> PillarsInScene = new SingleLinkedList<Pillar>();

        [SerializeField] private Transform FollowingOrigin;

        [SerializeField] private MonoBehaviour GrowingHeightProvider;
        [SerializeField] private MonoBehaviour LengthProvider;
        [SerializeField] private MonoBehaviour WidthProvider;
        [SerializeField] private MonoBehaviour GrowingStepProvider;

        private IConstProvider<float> ParsedGrowingHeightProvider;
        private IConstProvider<float> ParsedLengthProvider;
        private IConstProvider<float> ParsedWidthProvider;
        private IConstProvider<float> ParsedGrowingStepProvider;

        private float GrowingHeight;

        public Transform FollowingOrigin_ => FollowingOrigin;

        private bool IsGrowing = true;

        private void Awake()
        {
            ParsedGrowingHeightProvider = GrowingHeightProvider as IConstProvider<float>;
            if (ParsedGrowingHeightProvider == null)
                throw new NullReferenceException("Missing GrowingHeightProvider.");

            ParsedLengthProvider = LengthProvider as IConstProvider<float>;
            if (ParsedLengthProvider == null)
                throw new NullReferenceException("Missing LengthProvider.");

            ParsedWidthProvider=WidthProvider as IConstProvider<float>;
            if (ParsedWidthProvider == null)
                throw new NullReferenceException("Missing WidthProvider.");

            ParsedGrowingStepProvider=GrowingStepProvider as IConstProvider<float>;
            if (ParsedGrowingStepProvider == null)
                throw new NullReferenceException("Missing GrowingStepProvider.");

            PillarsInScene.AddLast(this);
            if (PillarsInScene.Count_ > GlobalConsts.inst_.MaxPillarsInScene)
                PillarsInScene[PillarsInScene.Count_ - GlobalConsts.inst_.MaxPillarsInScene-1].RemoveSmoothly();
            //Remove old pillars
        }
        private void Start()
        {
            transform.localScale = new Vector3
                (ParsedLengthProvider.GetValue(),
                transform.localScale.y,
                ParsedWidthProvider.GetValue());
            GrowingHeight = ParsedGrowingHeightProvider.GetValue();
        }
        private void FixedUpdate()
        {
            if (IsGrowing)
            {
                if (transform.position.y < GrowingHeight)
                    transform.position += Vector3.up * ParsedGrowingStepProvider.GetValue();
                else
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        GrowingHeight,
                        transform.position.z);
                    enabled = false;
                    GrowingDoneEvent();
                }
            }
            else
            {
                if (transform.position.y > 0)
                    transform.position -= Vector3.up * ParsedGrowingStepProvider.GetValue();
                else
                {
                    PillarsInScene.Remove(this);
                    Destroy(gameObject);
                }
            }
        }
        private void RemoveSmoothly()
        {
            if (IsGrowing)
            {
                IsGrowing = false;
                enabled = true;
            }
        }
    }
}
