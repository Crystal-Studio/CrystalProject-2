using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalStudioTools
{
    public class Tools
    {
        /// <summary>
        /// Get the direction between 2 vectors
        /// </summary>
        /// <param name="v1">Start point</param>
        /// <param name="v2">Target point</param>
        /// <returns></returns>
        public static Vector3 GetDirection(Vector3 v1, Vector3 v2)
        {
            var heading = v1 - v2;
            var distance = heading.magnitude;
            var direction = heading / distance;

            return direction;
        }
    }
}

