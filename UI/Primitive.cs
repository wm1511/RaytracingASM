using LibCS;
namespace UI;

internal struct HitRecord
{
    public double T;
    public Vec3 IntersectionPoint;
    public Vec3 Normal;
    public Material Material;
}

internal class Sphere
{
    private readonly Vec3 _center;
    private readonly double _radius;
    private readonly Material _material;

    public Sphere(Vec3 center, double radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    public bool Hit(Ray ray, double tMax, ref HitRecord record)
    {
        record.T = Intersection.IntersectSphere(ray.Origin, ray.Direction, _center, _radius, tMax);
        if (record.T > 0.0)
        {
            record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
            record.Normal = (record.IntersectionPoint - _center) / _radius;
            record.Material = _material;
            return true;
        }
        return false;
    }
}

//internal class Triangle : Primitive
//{
//    private readonly Vec3 _v0, _v1, _v2;
//    private readonly Material _material;

//    public Triangle(Vec3 v0, Vec3 v1, Vec3 v2, Material material)
//    {
//        _v0 = v0;
//        _v1 = v1;
//        _v2 = v2;
//        _material = material;
//    }

//    public override bool Hit(Ray ray, double tMin, double tMax, ref HitRecord record)
//    {
//        // MT

//        //var v0v1 = _v1 - _v0; 
//        //var v0v2 = _v2 - _v0; 
//        //var pvec = Vec3.Cross(ray.Direction, v0v2); 
//        //var det = Vec3.Dot(v0v1, pvec); 

//        //if (det < double.Epsilon) return false; 

//        //var invDet = 1 / det; 
 
//        //var tvec = ray.Origin - _v0; 
//        //var u = Vec3.Dot(tvec, pvec) * invDet; 
//        //if (u < 0 || u > 1) return false; 
 
//        //var qvec = Vec3.Cross(tvec, v0v1); 
//        //var v = Vec3.Dot(ray.Direction, qvec) * invDet; 
//        //if (v < 0 || u + v > 1) return false; 
 
//        //record.T = Vec3.Dot(v0v2, qvec) * invDet;
//        //record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
//        //record.Normal = Vec3.Cross(v0v2, v0v1);
//        //record.Material = _material;
 
//        //return true; 

//        // Geometric

//        //var v0v1 = _v1 - _v0;
//        //var v0v2 = _v2 - _v0;
//        //record.Normal = Vec3.Normalize(Vec3.Cross(v0v1, v0v2));

//        //var nDotDir = Vec3.Dot(record.Normal, ray.Direction);
//        //if (Math.Abs(nDotDir) < double.Epsilon)
//        //    return false;

//        //var d = Vec3.Dot(-record.Normal, _v0);
//        //record.T = -(Vec3.Dot(record.Normal, ray.Origin) + d) / nDotDir;

//        //if (record.T < 0)
//        //    return false;

//        //record.IntersectionPoint = ray.Origin + record.T * ray.Direction;

//        //var edge0 = _v1 - _v0;
//        //var vp0 = record.IntersectionPoint - _v0;
//        //if (Vec3.Dot(record.Normal, Vec3.Cross(edge0, vp0)) < 0)
//        //    return false;

//        //var edge1 = _v2 - _v1;
//        //var vp1 = record.IntersectionPoint - _v1;
//        //if (Vec3.Dot(record.Normal, Vec3.Cross(edge1, vp1)) < 0)
//        //    return false;

//        //var edge2 = _v0 - _v2;
//        //var vp2 = record.IntersectionPoint - _v2;
//        //if (Vec3.Dot(record.Normal, Vec3.Cross(edge2, vp2)) < 0)
//        //    return false;

//        //record.Material = _material;
//        //return true;
//    }
//}