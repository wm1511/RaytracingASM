using LibCs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace UI;

internal struct HitRecord
{
    public float T;
    public Vector3 IntersectionPoint;
    public Vector3 Normal;
    public Material Material;
}

internal class Sphere
{
    private readonly Vector3 _center;
    private readonly float _radius;
    private readonly Material _material;
    public static Func<Vector3, Vector3, Vector3, float, float, float> IntersectionFunc = Intersection.IntersectSphereCs;

    public Sphere(Vector3 center, float radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    public bool Hit(Ray ray, float tMax, ref HitRecord record)
    {
        record.T = IntersectionFunc(ray.Origin, ray.Direction, _center, _radius, tMax);
        if (record.T > 0.0)
        {
            record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
            record.Normal = (record.IntersectionPoint - _center) / _radius;
            record.Material = _material;
            return true;
        }
        return false;
    }

    [DllImport("LibAsm.dll")]
    public static extern float IntersectSphereAsm(Vector3 origin, Vector3 direction, Vector3 center, float radius, float tMax);
}