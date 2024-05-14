
using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
using System;
using UnityEngine;

namespace Chalk
{
    public sealed class Card : StaticAnimatedObj
    {
        [SerializeField] private AudioSource NoiseSource;

        protected override void InteractionAction()
        {
            if (NoiseSource.isPlaying)
                NoiseSource.Stop();
            else
                NoiseSource.Play();
        }
    }
}
