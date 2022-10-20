namespace LibCS;

public static class Intersection
{
    public static double IntersectSphere(Vec3 origin, Vec3 direction, Vec3 center, double radius, double tMax)
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
            if (solution1 < tMax && solution1 > 0.001)
                return solution1;

            var solution2 = (-b + sqrtDeterminant) / a;
            if (solution2 < tMax && solution2 > 0.001)
                return solution2;
        }
        return 0.0;
    }
}
