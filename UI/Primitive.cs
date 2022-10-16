using Accessibility;
using LibCS;
namespace UI;

internal struct HitRecord
{
    public double T;
    public Vec3 IntersectionPoint;
    public Vec3 Normal;
    public Material Material;
}

internal abstract class Primitive
{
    public abstract bool Hit(Ray ray, double tMin, double tMax, ref HitRecord record);
}

internal class Sphere : Primitive
{
    private Vec3 _center;
    private double _radius;
    private Material _material;

    public Sphere(Vec3 center, double radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    public override bool Hit(Ray ray, double tMin, double tMax, ref HitRecord record)
    {
        var oc = ray.Origin - _center;
        var a = Vec3.Dot(ray.Direction, ray.Direction);
        var b = Vec3.Dot(oc, ray.Direction);
        var c = Vec3.Dot(oc, oc) - _radius * _radius;
        var determinant = b * b - a * c;

        if (determinant > 0)
        {
            var sqrtDeterminant = Math.Sqrt(determinant);
            var solution1 = (-b - sqrtDeterminant) / a;
            if (solution1 < tMax && solution1 > 0.01)
            {
                record.T = solution1;
                record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
                record.Normal = (record.IntersectionPoint - _center) / _radius;
                record.Material = _material;
                return true;
            }

            var solution2 = (-b + sqrtDeterminant) / a;
            if (solution2 < tMax && solution2 > 0.01)
            {
                record.T = solution2;
                record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
                record.Normal = (record.IntersectionPoint - _center) / _radius;
                record.Material = _material;
                return true;
            }
        }
        return false;

        //TODO Przeniesienie do biblioteki i sprawdzenie przekazywania argumentów (prawdopodobnie tylko t z record)
        //var isIntersected = Intersection.IntersectSphere(ray.Origin, ray.Direction, ref _center, ref record.Normal, ref record.T, ref _radius, ref tMax, ref tMin);
        //if (isIntersected)
        //    record.Material = _material;
        //return isIntersected;
    }
}

internal class Triangle : Primitive
{
    private readonly Vec3 _v0, _v1, _v2;
    private readonly Material _material;

    public Triangle(Vec3 v0, Vec3 v1, Vec3 v2, Material material)
    {
        _v0 = v0;
        _v1 = v1;
        _v2 = v2;
        _material = material;
    }

    public override bool Hit(Ray ray, double tMin, double tMax, ref HitRecord record)
    {
        var v0v1 = _v1 - _v0;
        var v0v2 = _v2 - _v0;
        record.Normal = Vec3.Normalize(Vec3.Cross(v0v1, v0v2));

        var nDotDir = Vec3.Dot(record.Normal, ray.Direction);
        if (Math.Abs(nDotDir) < double.Epsilon)
            return false;

        var d = Vec3.Dot(-record.Normal, _v0);
        record.T = -(Vec3.Dot(record.Normal, ray.Origin) + d) / nDotDir;

        if (record.T < 0)
            return false;

        record.IntersectionPoint = ray.Origin + record.T * ray.Direction;

        var edge0 = _v1 - _v0;
        var vp0 = record.IntersectionPoint - _v0;
        if (Vec3.Dot(record.Normal, Vec3.Cross(edge0, vp0)) < 0)
            return false;

        var edge1 = _v2 - _v1;
        var vp1 = record.IntersectionPoint - _v1;
        if (Vec3.Dot(record.Normal, Vec3.Cross(edge1, vp1)) < 0)
            return false;

        var edge2 = _v0 - _v2;
        var vp2 = record.IntersectionPoint - _v2;
        if (Vec3.Dot(record.Normal, Vec3.Cross(edge2, vp2)) < 0)
            return false;

        record.Material = _material;
        return true;
    }
}