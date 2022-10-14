namespace LibCS;

public static class Intersection
    {
        public static bool IntersectSphere(Vec3 origin, Vec3 direction, ref Vec3 center, ref Vec3 normal, ref double radius, ref double t, ref double tMax, ref double tMin)
        {
            var oc = origin - center;
            var a = Vec3.Dot(direction, direction);
            var b = Vec3.Dot(oc, direction);
            var c = Vec3.Dot(oc, oc) - radius * radius;
            var determinant = b * b - a * c;

            if (determinant > 0)
            {
                var sqrtDeterminant = Math.Sqrt(determinant);
                var solution1 = (-b - sqrtDeterminant) / a;
                if (solution1 < tMax && solution1 > tMin)
                {
                    t = solution1;
                    var intersection = origin + t * direction;
                    normal = (intersection - center) / radius;
                    return true;
                }

                var solution2 = (-b + sqrtDeterminant) / a;
                if (solution2 < tMax && solution2 > tMin)
                {
                    t = solution2;
                    var intersection = origin + t * direction;
                    normal = (intersection - center) / radius;
                    return true;
                }
            }
            return false;
        }
    }


