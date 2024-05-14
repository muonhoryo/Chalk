
using System;
using System.Collections;
using UnityEngine;

namespace Chalk
{
    public sealed class TempleObject : MonoBehaviour
    {
        [SerializeField] private float LifeTime;

        private void Awake()
        {
            if (LifeTime <= 0)
                throw new ArgumentException("Incorrect LifeTime.");

            StartCoroutine(DeathDelay());
        }
        private IEnumerator DeathDelay()
        {
            yield return new WaitForSeconds(LifeTime);
            Destroy(gameObject);
        }
    }
}
