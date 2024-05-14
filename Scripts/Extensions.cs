

using UnityEngine;

namespace Chalk
{
    public static class Extensions
    {
        public static Vector3 StereoRotationFromDirection(this Vector3 direction)
        {
            return Quaternion.LookRotation(direction).eulerAngles;
        }
    }
}