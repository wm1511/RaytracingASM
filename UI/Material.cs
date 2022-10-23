using System.Numerics;

namespace UI;

internal abstract class Material
{
    protected static readonly Random Rng = new();

    public abstract bool Scatter(Ray incidentRay, HitRecord rec, out Vector3 attenuation, out Ray scatteredRay);

    protected static Vector3 RandomInUnitSphere()
    {
        Vector3 p;
        do
        {
            p = 2 * new Vector3((float)Rng.NextDouble(), (float)Rng.NextDouble(), (float)Rng.NextDouble()) - new Vector3(1, 1, 1);
        }
        while (p.LengthSquared() >= 1);
        return p;
    }

    protected static bool Refract(Vector3 v, Vector3 n, float invRefractionIndex, out Vector3 refractedDirection)
    {
        var uv = Vector3.Normalize(v);
        var dt = Vector3.Dot(uv, n);
        var determinant = 1 - invRefractionIndex * invRefractionIndex * (1 - dt * dt);

        if (determinant > 0)
        {
            refractedDirection = invRefractionIndex * (uv - n * dt) - n * MathF.Sqrt(determinant);
            return true;
        }

        refractedDirection = new Vector3(0, 0, 0);
        return false;
    }

    protected static Vector3 Reflect(Vector3 direction, Vector3 normal) => direction - 2 * Vector3.Dot(direction, normal) * normal;

    protected static float Schlick(float cosine, float refractionIndex)
    {
        var r0 = (1 - refractionIndex) / (1 + refractionIndex);
        r0 *= r0;
        return r0 + (1 - r0) * MathF.Pow(1 - cosine, 5);
    }
}

internal class Diffuse : Material
{
    private readonly Vector3 _diffuse;

    public Diffuse(Vector3 diffuse)
    {
        _diffuse = diffuse;
    }

    public override bool Scatter(Ray incidentRay, HitRecord rec, out Vector3 attenuation, out Ray scatteredRay)
    {
        var targetOnUnitSphere = rec.IntersectionPoint + rec.Normal + RandomInUnitSphere();
        scatteredRay = new Ray(rec.IntersectionPoint, targetOnUnitSphere - rec.IntersectionPoint);
        attenuation = _diffuse;
        return true;
    }
}

internal class Metal : Material
{
    private readonly Vector3 _diffuse;
    private readonly float _fuzziness;

    public Metal(Vector3 diffuse, float fuzziness)
    {
        _diffuse = diffuse;
        _fuzziness = fuzziness < 1 ? fuzziness : 1;
    } 

    public override bool Scatter(Ray incidentRay, HitRecord rec, out Vector3 attenuation, out Ray scatteredRay)
    {
        var reflected = Reflect(Vector3.Normalize(incidentRay.Direction), rec.Normal);
        scatteredRay = new Ray(rec.IntersectionPoint, reflected + _fuzziness * RandomInUnitSphere());
        attenuation = _diffuse;
        return Vector3.Dot(scatteredRay.Direction, rec.Normal) > 0;
    }
}

internal class Dielectric : Material
{
    private readonly float _refractionIndex;

    public Dielectric(float refractionIndex)
    {
        _refractionIndex = refractionIndex;
    }

    public override bool Scatter(Ray incidentRay, HitRecord rec, out Vector3 attenuation, out Ray scatteredRay)
    {
        attenuation = new Vector3(1, 1, 1);
        Vector3 outwardNormal;
        float invRefractionIndex;
        float cosine;
        var reflectedRay = Reflect(incidentRay.Direction, rec.Normal);

        if (Vector3.Dot(incidentRay.Direction, rec.Normal) > 0)
        {
            outwardNormal = -1 * rec.Normal;
            invRefractionIndex = _refractionIndex;
            cosine = _refractionIndex * Vector3.Dot(incidentRay.Direction, rec.Normal) / incidentRay.Direction.Length();
        }
        else
        {
            outwardNormal = rec.Normal;
            invRefractionIndex = 1 / _refractionIndex;
            cosine = -Vector3.Dot(incidentRay.Direction, rec.Normal) / incidentRay.Direction.Length();
        }

        var reflectionProbability = Refract(incidentRay.Direction, outwardNormal, invRefractionIndex, out var refractedRay) 
            ? Schlick(cosine, _refractionIndex) 
            : 1;

        scatteredRay = (float)Rng.NextDouble() < reflectionProbability
            ? new Ray(rec.IntersectionPoint, reflectedRay)
            : new Ray(rec.IntersectionPoint, refractedRay);

        return true;
    }
}