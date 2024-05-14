using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Player
{
    public sealed class SimpleMovingDirection : MonoBehaviour,IMovingDirectionCalculator
    {
        public Vector3 GetDirection(Vector2 input)
        {
            return new Vector3(input.x, 0, input.y);
        }
    }
}
