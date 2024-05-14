

using System;
using UnityEngine;

namespace Chalk
{
    public sealed class EndGameArea : MonoBehaviour
    {
        public event Action PlayerEnterEvent = delegate { };

        [SerializeField] private GameObject Player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Player)
                PlayerEnterEvent();
        }
    }
}