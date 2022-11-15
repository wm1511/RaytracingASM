using System.Numerics;

namespace LibCs;

public static class Intersection
{
    // Funkcja wyznacza odległość (t), po przebyciu której promień napotka punkt przecięcia z ze sferą umieszczoną
    // w przestrzeni lub zwróci informację o braku przecięcia. Obliczona wartość jest wykorzystywana do dalszych
    // obliczeń w celu wygenerowania obrazu.
    public static float IntersectSphereCs(Vector3 origin, Vector3 direction, Vector3 center, float radius, float tMax)
    {
        var oc = origin - center;
        var a = Vector3.Dot(direction, direction);
        var b = Vector3.Dot(oc, direction);
        var c = Vector3.Dot(oc, oc) - radius * radius;
        var determinant = b * b - a * c;

        if (determinant > 0)
        {
            var sqrtDeterminant = MathF.Sqrt(determinant);
            var solution1 = (-b - sqrtDeterminant) / a;
            if (solution1 < tMax && solution1 > 0.001f)
                return solution1;

            var solution2 = (-b + sqrtDeterminant) / a;
            if (solution2 < tMax && solution2 > 0.001f)
                return solution2;
        }
        return 0.0f;
    }
}
