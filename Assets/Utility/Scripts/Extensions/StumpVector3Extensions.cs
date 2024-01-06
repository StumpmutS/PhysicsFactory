using System;
using UnityEngine;

namespace Utility.Scripts.Extensions
{
    public static class StumpVector3Extensions
    {
        public static bool SetToCursorToWorldPosition(this ref Vector3 vector3)
        {
            var ray = MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return false;
            vector3 = hit.point;
            return true;
        }
        
        public static bool SetToCursorToWorldPosition(this ref Vector3 vector3, LayerMask layer)
        {
            var ray = MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, layer)) return false;
            vector3 = hit.point;
            return true;
        }

        public static Vector3 ClampValues(this Vector3 vector3, float min, float max)
        {
            if (vector3.x < min) vector3.x = min;
            if (vector3.x > max) vector3.x = max;
            if (vector3.y < min) vector3.y = min;
            if (vector3.y > max) vector3.y = max;
            if (vector3.z < min) vector3.z = min;
            if (vector3.z > max) vector3.z = max;
            return vector3;
        }

        public static Vector3 Abs(this Vector3 vector3)
        {
            vector3.x = Mathf.Abs(vector3.x);
            vector3.y = Mathf.Abs(vector3.y);
            vector3.z = Mathf.Abs(vector3.z);
            return vector3;
        }

        public static Vector3 Invert(this Vector3 vector3)
        {
            return new Vector3(1 / vector3.x, 1 / vector3.y, 1 / vector3.z);
        }

        public static Vector3 IsolateAxis(this Vector3 vector3, Vector3 exclude = default, Vector3 prioritize = default)
        {
            //exclusion
            var x = exclude.x is > .01f or < -.01f ? 0 : Mathf.Abs(vector3.x);
            var y = exclude.y is > .01f or < -.01f ? 0 : Mathf.Abs(vector3.y);
            var z = exclude.z is > .01f or < -.01f ? 0 : Mathf.Abs(vector3.z);
            
            //prioritization
            bool xPrio = prioritize.x > .01f && x > .01f;
            bool yPrio = prioritize.y > .01f && y > .01f;
            bool zPrio = prioritize.z > .01f && z > .01f;
            if (!xPrio && (yPrio || zPrio)) x = 0; 
            if (!yPrio && (xPrio || zPrio)) y = 0;
            if (!zPrio && (xPrio || yPrio)) z = 0;

            //sorting
            if (y >= x) x = 0;
            if (z >= x) x = 0;
            if (x > y) y = 0;
            if (z > y) y = 0;
            if (x > z) z = 0;
            if (y >= z) z = 0;

            //use original sign
            return new Vector3(vector3.x < 0 ? -x : x, vector3.y < 0 ? -y : y, vector3.z < 0 ? -z : z);
        }
    }
}
