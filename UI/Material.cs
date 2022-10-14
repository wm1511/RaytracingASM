using LibCS;

namespace UI;

internal abstract class Material
{
    protected static readonly Random Rng = new();

    public abstract bool Scatter(Ray incidentRay, HitRecord rec, out Vec3 attenuation, out Ray scatteredRay);

    protected static Vec3 RandomInUnitSphere()
    {
        Vec3 p;
        do
        {
            p = 2 * new Vec3(Rng.NextDouble(), Rng.NextDouble(), Rng.NextDouble()) - new Vec3(1, 1, 1);
        }
        while (p.SquaredLength() >= 1);
        return p;
    }

    protected static bool Refract(Vec3 v, Vec3 n, double invRefractionIndex, out Vec3 refractedRay)
    {
        var uv = Vec3.Normalize(v);
        var dt = Vec3.Dot(uv, n);
        var determinant = 1 - invRefractionIndex * invRefractionIndex * (1 - dt * dt);

        if (determinant > 0)
        {
            refractedRay = invRefractionIndex * (uv - n * dt) - n * Math.Sqrt(determinant);
            return true;
        }

        refractedRay = new Vec3(0, 0, 0);
        return false;
    }

    protected static Vec3 Reflect(Vec3 ray, Vec3 normal) => ray - 2 * Vec3.Dot(ray, normal) * normal;

    protected static double Schlick(double cosine, double refractionIndex)
    {
        var r0 = (1 - refractionIndex) / (1 + refractionIndex);
        r0 *= r0;
        return r0 + (1 - r0) * Math.Pow(1 - cosine, 5);
    }
}

internal class Diffuse : Material
{
    private readonly Vec3 _diffuse;

    public Diffuse(Vec3 diffuse)
    {
        _diffuse = diffuse;
    }

    public override bool Scatter(Ray incidentRay, HitRecord rec, out Vec3 attenuation, out Ray scatteredRay)
    {
        var targetOnUnitSphere = rec.IntersectionPoint + rec.Normal + RandomInUnitSphere();
        scatteredRay = new Ray(rec.IntersectionPoint, targetOnUnitSphere - rec.IntersectionPoint);
        attenuation = _diffuse;
        return true;
    }
}

internal class Metal : Material
{
    private readonly Vec3 _diffuse;
    private readonly double _fuzziness;

    public Metal(Vec3 diffuse, double fuzziness)
    {
        _diffuse = diffuse;
        _fuzziness = fuzziness < 1 ? fuzziness : 1;
    } 

    public override bool Scatter(Ray incidentRay, HitRecord rec, out Vec3 attenuation, out Ray scatteredRay)
    {
        var reflected = Reflect(Vec3.Normalize(incidentRay.Direction), rec.Normal);
        scatteredRay = new Ray(rec.IntersectionPoint, reflected + _fuzziness * RandomInUnitSphere());
        attenuation = _diffuse;
        return Vec3.Dot(scatteredRay.Direction, rec.Normal) > 0;
    }
}

internal class Dielectric : Material
{
    private readonly double _refractionIndex;

    public Dielectric(double refractionIndex)
    {
        _refractionIndex = refractionIndex;
    }

    public override bool Scatter(Ray incidentRay, HitRecord rec, out Vec3 attenuation, out Ray scatteredRay)
    {
        attenuation = new Vec3(1, 1, 1);
        Vec3 outwardNormal;
        double invRefractionIndex;
        double cosine;
        var reflectedRay = Reflect(incidentRay.Direction, rec.Normal);

        if (Vec3.Dot(incidentRay.Direction, rec.Normal) > 0)
        {
            outwardNormal = -1 * rec.Normal;
            invRefractionIndex = _refractionIndex;
            cosine = _refractionIndex * Vec3.Dot(incidentRay.Direction, rec.Normal) / incidentRay.Direction.Length();
        }
        else
        {
            outwardNormal = rec.Normal;
            invRefractionIndex = 1 / _refractionIndex;
            cosine = -Vec3.Dot(incidentRay.Direction, rec.Normal) / incidentRay.Direction.Length();
        }

        var reflectionProbability = Refract(incidentRay.Direction, outwardNormal, invRefractionIndex, out var refractedRay) 
            ? Schlick(cosine, _refractionIndex) 
            : 1;

        scatteredRay = Rng.NextDouble() < reflectionProbability
            ? new Ray(rec.IntersectionPoint, reflectedRay)
            : new Ray(rec.IntersectionPoint, refractedRay);

        return true;
    }
}