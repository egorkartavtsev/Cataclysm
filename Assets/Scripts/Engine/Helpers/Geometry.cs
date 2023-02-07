using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Helpers
{
    public static class Geometry
    {
        public static bool PointInCircle(Vector3 point, Vector3 center, float radius)
            => Math.Sqrt(Math.Pow(center.x - point.x, 2) + Math.Pow(center.z - point.z, 2)) <= radius;

        public static bool ValidAngle(Vector3 center, Vector3 point, Vector3 clickPoint, float rangeAng)
        {
            Vector2 targetV = VectorCoords(point, center);
            Vector2 attackV = VectorCoords(clickPoint, center);

            float degrees = Vector2.Angle(targetV, attackV);

            return degrees < rangeAng / 2f;
        }

        public static Vector3 GetEndpoing(Vector3 center, Vector3 clickPoint, float distance)
        {
            Vector2 vectMain = VectorCoords(clickPoint, center).normalized*distance;
            return new Vector3(vectMain.x + center.x, center.y, vectMain.y + center.z);
        }

        public static Vector2 VectorCoords(Vector3 point, Vector3 center)
            => new Vector2(point.x - center.x, point.z - center.z);

    }
}
